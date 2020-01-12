using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation;
using CsvHelper;
using Microsoft.Extensions.Configuration;

namespace ClaimsReserving.Services.Writer
{
    public class CsvOutput : ICsvOutput
    {
        private string OutputFilesPath { get; }
        private ISummaryRecord SummaryRecord { get; }

        public CsvOutput(IConfiguration configuration, ISummaryRecord summaryRecord)
        {
            OutputFilesPath = configuration.GetValue<string>("OutputFilesDirectory");
            SummaryRecord = summaryRecord;
        }

        public void Write(List<List<YearlyData>> recordsPerProduct, string outputFileName)
        {
            if (!recordsPerProduct.Any())
            {
                return;
            }

            using var writer = new StreamWriter(OutputFilesPath + "\\" + outputFileName);
            using var csv = new CsvWriter(writer);
            SetCsvHelperConfiguration(csv);

            var records = FlattenRecords(recordsPerProduct);

            SummaryRecord.Write(csv, records);

            MinAndMaxYears minAndMaxYears = new MinAndMaxYears
            {
                Minimum = records.Min(p => p.OriginYear),
                Maximum = records.Max(p => p.OriginYear)
            };

            new ProductClaimsRecord().WriteForAllProducts(csv, recordsPerProduct, minAndMaxYears);
        }

        private List<YearlyData> FlattenRecords(List<List<YearlyData>> recordsPerProduct)
        {
            var records = new List<YearlyData>();
            foreach (var productRecords in recordsPerProduct)
            {
                records.AddRange(productRecords);
            }

            return records;
        }

        private void SetCsvHelperConfiguration(CsvWriter csv)
        {
            csv.Configuration.HasHeaderRecord = false;
        }

        public string GetOutputFileName(string inputFileName)
        {
            const string csvExtension = ".csv";
            string outputName = inputFileName.Replace(csvExtension, "") + "Output";

            return outputName + csvExtension;
        }

    }
}
