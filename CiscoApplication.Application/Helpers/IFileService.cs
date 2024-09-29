using Microsoft.AspNetCore.Http;

namespace CiscoApplication.Application.Helpers
{
    internal interface IFileService
    {
        public (string, string) UploadFile(IFormFile formFilestring, DirectoriesEnum FileFor);
        public void MoveFile(DirectoriesEnum FileFor, string _fileName);
        public string GetFilePath(DirectoriesEnum FileFor, string _fileName);

    }
}
