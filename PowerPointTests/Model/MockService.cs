using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Download;
using Google.Apis.Upload;

namespace PowerPoint.Tests
{
    public class MockService : IGoogleDriveService
    {
        // 整合刪除與上傳
        public void DeleteAndUploadFile(string uploadFileName, string contentType)
        {
            throw new NotImplementedException();
        }

        // 刪檔案
        public void DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        // 下載檔案
        public void DownloadFile(Google.Apis.Drive.v2.Data.File fileToDownload, string downloadPath, Action<IDownloadProgress> downloadProgressChangedEventHandler = null)
        {
            throw new NotImplementedException();
        }

        // 找到檔案並下載
        public void FindAndDownloadFile(string downloadFileName, string downloadPath)
        {
            throw new NotImplementedException();
        }

        // 列出根目錄下的檔案
        public List<Google.Apis.Drive.v2.Data.File> ListRootFileAndFolder()
        {
            throw new NotImplementedException();
        }

        // 上傳檔案
        public void UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandler = null, Action<global::Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            throw new NotImplementedException();
        }
    }
}
