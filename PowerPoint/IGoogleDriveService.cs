using Google.Apis.Download;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;

namespace PowerPoint
{
    public interface IGoogleDriveService
    {
        // 刪檔案
        void DeleteFile(string fileId);

        // 下載檔案
        void DownloadFile(File fileToDownload, string downloadPath, Action<IDownloadProgress> downloadProgressChangedEventHandler = null);
        
        // 列出根目錄下的檔案
        List<File> ListRootFileAndFolder();

        // 上傳檔案
        void UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandler = null, Action<File> responseReceivedEventHandler = null);

        // 整合刪除與上傳
        void DeleteAndUploadFile(string uploadFileName, string contentType);

        // 找到檔案並下載
        void FindAndDownloadFile(string downloadFileName, string downloadPath);
    }
}