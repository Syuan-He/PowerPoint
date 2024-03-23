using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace PowerPoint
{
    public class FileManager
    {
        private const int ASYNC_WAITING_TIME = 10000;
        private const string ROOT_PATH = "..\\..\\..\\";

        // 存檔
        public static void Save(PagesForSave pagesFrame, IGoogleDriveService service)
        {
            string saveString = JsonSerializer.Serialize(pagesFrame);
            File.WriteAllText(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_PATH)) + DataString.FILE_NAME, saveString);
            if (service != null )
                service.DeleteAndUploadFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_PATH)) + DataString.FILE_NAME, DataString.CONTENT_TYPE);
            //Thread.Sleep(ASYNC_WAITING_TIME);
        }

        // 讀檔
        public static List<Shapes> Load(IGoogleDriveService service)
        {
            if (service != null)
                service.FindAndDownloadFile(DataString.FILE_NAME, Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_PATH)));
            string saveString = File.ReadAllText(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_PATH)) + DataString.FILE_NAME);
            PagesForSave pagesForSave = JsonSerializer.Deserialize<PagesForSave>(saveString);
            return pagesForSave.Turn2Pages();
        }
    }
}
