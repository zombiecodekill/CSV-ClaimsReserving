using System.Collections.Generic;

namespace ClaimsReserving.Models
{
    public class CsvModel
    {
        public List<FileModel> Files { get; set; } = new List<FileModel>();

        public string FilesFoundMessage { get; set; }
    }
}
