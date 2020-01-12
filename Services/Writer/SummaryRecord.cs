using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;
using CsvHelper;

namespace ClaimsReserving.Services.Writer
{
    public class SummaryRecord : ISummaryRecord
    {
        public void Write(CsvWriter csv, List<YearlyData> records)
        {
            var summaryRecord = Calculate(records);
            if (summaryRecord != null)
            {
                foreach (var s in summaryRecord)
                {
                    csv.WriteField(s);
                }
                csv.NextRecord();
            }
        }

        public List<int> Calculate(List<YearlyData> records)
        {
            if (!records.Any())
            {
                return null;
            }

            int minOriginYear = records.Min(r => r.OriginYear);

            int minDevelopmentYearAcrossAllProducts = records.Min(r => r.DevelopmentYear);
            int maxDevelopmentYearAcrossAllProducts = records.Max(r => r.DevelopmentYear);

            int developmentPeriod = maxDevelopmentYearAcrossAllProducts - minDevelopmentYearAcrossAllProducts + 1;

            return new List<int>() { minOriginYear, developmentPeriod };
        }
    }
}
