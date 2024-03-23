using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Download;
using Google.Apis.Drive.v2.Data;
using System.Net;

namespace PowerPoint
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly string[] _scope = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private const int KB = 0x400;
        private const int DOWNLOAD_CHUNK_SIZE = 256 * KB;
        private const int DOUBLE = 2;
        private const int ASYNC_WAIT_TIME = 10000;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;
        private UserCredential _credential;

        /// <summary>
        /// 創造一個Google Drive Service
        /// </summary>
        /// <param name="applicationName">應用程式名稱</param>
        /// <param name="clientSecretFileName">ClientSecret檔案名稱</param>
        public GoogleDriveService(string applicationName, string clientSecretFileName)
        {
            _applicationName = applicationName;
            _clientSecretFileName = clientSecretFileName;
            this.CreateNewService(applicationName, clientSecretFileName);
        }

        // 建立連線
        private void CreateNewService(string applicationName, string clientSecretFileName)
        {
            const string USER = "user";
            const string CREDENTIAL_FOLDER = ".credential/";
            UserCredential credential;
            using (FileStream stream = new FileStream(clientSecretFileName, FileMode.Open, FileAccess.Read))
            {
                string credentialPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credentialPath = Path.Combine(credentialPath, CREDENTIAL_FOLDER + applicationName);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, _scope, USER, CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }
            DriveService service = new DriveService(new BaseClientService.Initializer()
            { 
                HttpClientInitializer = credential,
                ApplicationName = applicationName });
            _credential = credential;
            DateTime now = DateTime.Now;
            _timeStamp = UNIXNowTimeStamp;
            _service = service;
        }

        private int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        //Check and refresh the credential if credential is out-of-date
        private void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                this.CreateNewService(_applicationName, _clientSecretFileName);
        }

        /// <summary>
        /// 查詢Google Drive 根目錄的檔案
        /// </summary>
        /// <returns>Google Drive File List</returns>
        public List<Google.Apis.Drive.v2.Data.File> ListRootFileAndFolder()
        {
            List<Google.Apis.Drive.v2.Data.File> returnList = new List<Google.Apis.Drive.v2.Data.File>();
            const string ROOT_QUERY_STRING = "'root' in parents";

            try
            {
                returnList = ListFileAndFolderWithQueryString(ROOT_QUERY_STRING);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return returnList;
        }

        /// <summary>
        /// 使用QueryString 查詢檔案 回傳一List
        /// </summary>
        /// <param name="queryString">QueryString，使用方法: https://developers.google.com/drive/web/search-parameters </param>
        /// <returns>含有Google Drive File 格式的 List</returns>
        private List<Google.Apis.Drive.v2.Data.File> ListFileAndFolderWithQueryString(string queryString)
        {
            List<Google.Apis.Drive.v2.Data.File> returnList = new List<Google.Apis.Drive.v2.Data.File>();
            this.CheckCredentialTimeStamp();
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = queryString;
            do
            {
                try
                {
                    FileList fileList = listRequest.Execute();
                    returnList.AddRange(fileList.Items);
                    listRequest.PageToken = fileList.NextPageToken;
                }
                catch (Exception exception)
                {
                    listRequest.PageToken = null;
                    throw exception;
                }
            } while (!String.IsNullOrEmpty(listRequest.PageToken));
            return returnList;
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="uploadFileName">上傳的檔案名稱 </param>
        /// <param name="contentType">上傳的檔案種類，請參考MIME Type : http://www.iana.org/assignments/media-types/media-types.xhtml </param>
        /// <param name="uploadProgressEventHandler"> 上傳進度改變時呼叫的函式</param>
        /// <param name="responseReceivedEventHandler">收到回應時呼叫的函式 </param>
        /// <returns>上傳成功，回傳上傳成功的 Google Drive 格式之File</returns>
        public void UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            FileStream uploadStream = new FileStream(uploadFileName, FileMode.Open, FileAccess.Read);
            string title = GetFileName(uploadFileName);
            FilesResource.InsertMediaUpload insertRequest = _service.Files.Insert(GetFile(title), uploadStream, contentType);
            insertRequest.ChunkSize = FilesResource.InsertMediaUpload.MinimumChunkSize * DOUBLE;
            if (uploadProgressEventHandler != null)
                insertRequest.ProgressChanged += uploadProgressEventHandler;
            if (responseReceivedEventHandler != null)
                insertRequest.ResponseReceived += responseReceivedEventHandler;
            UploadTry(insertRequest, uploadStream);
        }

        // 取得檔案名稱
        string GetFileName(string uploadFileName)
        {
            const char SPLASH = '\\';
            string title = "";

            if (uploadFileName.LastIndexOf(SPLASH) != -1)
                title = uploadFileName.Substring(uploadFileName.LastIndexOf(SPLASH) + 1);
            else
                title = uploadFileName;
            return title;
        }

        // 取得 title
        Google.Apis.Drive.v2.Data.File GetFile(string title)
        {
            
            Google.Apis.Drive.v2.Data.File fileToInsert = new Google.Apis.Drive.v2.Data.File
            { 
                Title = title };

            return fileToInsert;
        }

        // Upload 的 Try, catch
        void UploadTry(FilesResource.InsertMediaUpload insertRequest, FileStream uploadStream)
        {
            this.CheckCredentialTimeStamp();
            try
            {
                insertRequest.Upload();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                uploadStream.Close();
            }
        }

        // 整合刪除與上傳
        public void DeleteAndUploadFile(string uploadFileName, string contentType)
        {
            List<Google.Apis.Drive.v2.Data.File> fileList = ListRootFileAndFolder();
            string title = GetFileName(uploadFileName);
            Google.Apis.Drive.v2.Data.File foundFile = fileList.Find(item =>
            { 
                return item.Title == title;
            });
            if (foundFile != null)
                DeleteFile(foundFile.Id);
            UploadFile(uploadFileName, contentType);
        }

        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="fileToDownload">欲下載的檔案(Google Drive File) 一般會從List取得</param>
        /// <param name="downloadPath">下載到路徑</param>
        /// <param name="downloadProgressChangedEventHandler">當下載進度改變時，呼叫這個函式</param>
        public void DownloadFile(Google.Apis.Drive.v2.Data.File fileToDownload, string downloadPath, Action<IDownloadProgress> downloadProgressChangedEventHandler = null)
        {
            const string SPLASH = @"\";

            CheckCredentialTimeStamp();
            if (!String.IsNullOrEmpty(fileToDownload.DownloadUrl))
            {
                try
                {
                    Task<byte[]> downloadByte = _service.HttpClient.GetByteArrayAsync(fileToDownload.DownloadUrl);
                    byte[] byteArray = downloadByte.Result;
                    string downloadPosition = downloadPath + SPLASH + fileToDownload.Title;
                    System.IO.File.WriteAllBytes(downloadPosition, byteArray);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        // 找到檔案並下載
        public void FindAndDownloadFile(string downloadFileName, string downloadPath)
        {
            List<Google.Apis.Drive.v2.Data.File> fileList = ListRootFileAndFolder();
            Google.Apis.Drive.v2.Data.File foundFile = fileList.Find(item =>
            { 
                return item.Title == downloadFileName;
            });
            DownloadFile(foundFile, downloadPath);
        }

        /// <summary>
        /// 刪除符合FileID的檔案
        /// </summary>
        /// <param name="fileId">欲刪除檔案的FileID</param>
        public void DeleteFile(string fileId)
        {
            CheckCredentialTimeStamp();
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
