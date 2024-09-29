namespace CiscoApplication.Application.DTOs
{
    public class ExportedExcelDto
    {
        public string FilePath { get; private set; }
        public int TotalRecords { get; private set; }

        public ExportedExcelDto(string filePath, int totalRecords)
        {
            FilePath = filePath;
            TotalRecords = totalRecords;
        }
    }
}
