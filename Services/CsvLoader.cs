using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ClaimsReserving.Models;
using CsvHelper;
using Microsoft.Extensions.Configuration;

namespace ClaimsReserving.Services
{
    public class CsvLoader :
        ICsvLoader
    {
        private readonly string _path;
        private readonly IDirectoryFinder _directoryFinder;

        public CsvLoader(IDirectoryFinder directoryFinder, IConfiguration configuration)
        {
            _directoryFinder = directoryFinder;
            _path = configuration.GetValue<string>("InputFilesDirectory");
        }


        public string FindFilesMessage()
        {
            if (_directoryFinder.Exists(_path))
            {
                return "The following input files are available:";
                
            }
            return "Could not find InputFiles directory";
        }

        public List<FileModel> GetFiles()
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(_path);

            var files = new List<FileModel>();

            for (int i = 0; i < fileEntries.Length; i++)
            {
                string filePath = fileEntries[i];
                string fileName = GetFileNameFromPath(filePath);
                files.Add(new FileModel { Id = i, Name = fileName });
            }

            return files;
        }

        public FileModel GetFileById(int id)
        {
            var files = GetFiles();

            return files.FirstOrDefault(f => f.Id == id);
        }

        private string GetFileNameFromPath(string file)
        {
            return file.Replace(_path, "").Substring(1);
        }

        private void SetCsvHelperConfiguration(CsvReader csv)
        {
            csv.Configuration.IgnoreBlankLines = true;
            csv.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
            csv.Configuration.HasHeaderRecord = true;

            csv.Configuration.PrepareHeaderForMatch = (header, index) => Regex.Replace(header, @"\s", string.Empty);
        }

        public List<YearlyData> LoadCsvFile(string filePath)
        {
            var records = new List<YearlyData>();

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader);
            csv.Read();
            SetCsvHelperConfiguration(csv);

            try
            {
                csv.ReadHeader();
            }
            catch (ReaderException)
            {
                return records; // If the header record is missing we cannot load the file
            }
            
            while (csv.Read())
            {
                var record = new YearlyData
                {
                    ProductName = csv.GetField<string>("Product"),
                    OriginYear = csv.GetField<int>("Origin Year"),
                    DevelopmentYear = csv.GetField<int>("Development Year"),
                    IncrementalValue = csv.GetField<decimal?>("Incremental Value"),
                };
                records.Add(record);
            }

            return records;
        }
    }
}
