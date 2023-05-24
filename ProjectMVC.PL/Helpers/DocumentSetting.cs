using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Helpers
{
    public static class DocumentSetting
    {
        public static async Task<string> UploadFile(IFormFile file, string FolderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

           using var fs = new FileStream(filePath, FileMode.Create);
        
           await file.CopyToAsync(fs);

            return fileName;
        }

        public static void DeleteImage(string fileName, string FolderName)
        {
            if(fileName is not null && FolderName is not null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", FolderName, fileName);
                if(File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }
        }


    }
}
