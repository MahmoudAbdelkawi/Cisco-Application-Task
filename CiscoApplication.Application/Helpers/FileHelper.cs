using Microsoft.AspNetCore.Http;

namespace CiscoApplication.Application.Helpers
{
    internal class FileHelper : IFileService
    {
        private readonly string FileDirectory = Directory.GetCurrentDirectory();
        private readonly string MainFolder = "wwwroot/";
        private readonly string DeletedFolder = "Deleted/";

        public (string, string) UploadFile(IFormFile File, DirectoriesEnum FileFor)
        {
            //Get File Upload For
            string UploadDirectory = FileFor.ToString();
            //Get Full Folder Path
            string FolderFullPath = Path.Combine(FileDirectory, MainFolder, UploadDirectory);
            //Make File Name Unique
            string FileName = string.Concat(Guid.NewGuid().ToString().Replace("-", string.Empty), File.FileName);
            //Get File Total Path on System
            string filePath = Path.Combine(FolderFullPath, FileName);
            //Get File Extension
            string Extension = Path.GetExtension(File.FileName).Remove(0, 1);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            File.CopyTo(fileStream);
            return (FileName, Extension);
        }
        public void MoveFile(DirectoriesEnum FileFor, string _fileName)
        {
            string OldPath = GetFilePath(FileFor, _fileName);
            string NewDirectory = Path.Combine(FileDirectory, MainFolder, DeletedFolder);
            string newPath = Path.Combine(NewDirectory, _fileName);
            File.Move(OldPath, newPath, true);
        }
        public string GetFilePath(DirectoriesEnum FileFor, string _fileName)
        {
            string UploadDirectory = FileFor.ToString();
            string FolderFullPath = Path.Combine(FileDirectory, MainFolder, UploadDirectory);
            string filePath = Path.Combine(FolderFullPath, _fileName);
            return filePath ?? "Not Found";
        }
    }
}
