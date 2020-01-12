using ClaimsReserving.Models;
using System.Collections.Generic;

namespace ClaimsReserving.Services
{
    public interface ICsvLoader
    {
        List<FileModel> GetFiles();
        string FindFilesMessage();

        FileModel GetFileById(int id);
        List<YearlyData> LoadCsvFile(string filepath);
    }
}