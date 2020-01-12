using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;
using CsvHelper;

namespace ClaimsReserving.Services.Writer
{
    public class ProductClaimsRecord
    {
        public void WriteForAllProducts(CsvWriter csv, List<List<YearlyData>> recordsPerProduct, MinAndMaxYears minAndMaxYears)
        {
            foreach (var productRecords in recordsPerProduct)
            {
                Write(csv, productRecords, minAndMaxYears);
            }
        }

        public void Write(CsvWriter csv, List<YearlyData> productRecords, MinAndMaxYears minAndMaxYears)
        {
            if (productRecords.Any())
            {
                string productName = productRecords.Select(r => r.ProductName).First();
                csv.WriteField(productName);

                var runningTotals = RunningTotals(productRecords, minAndMaxYears);

                foreach (var value in runningTotals)
                {
                    csv.WriteField(value);
                }

                csv.NextRecord();
            }
        }

        public List<string> RunningTotals(List<YearlyData> productRecords, MinAndMaxYears minAndMaxYears)
        {
            List<string> runningTotals = new List<string>();
            int range = minAndMaxYears.Maximum - minAndMaxYears.Minimum;

            for (int y = 0; y <= range; y++)
            {
                decimal runningTotal = 0m;

                for (int x = 0; x <= range - y; x++)
                {
                    var increment = FindIncrementalValue(
                        productRecords,
                        minAndMaxYears.Minimum, x, y);

                    runningTotal += increment;

                    runningTotals.Add(FormattedNumber(runningTotal));
                }
            }

            return runningTotals;
        }

        public decimal FindIncrementalValue(IEnumerable<YearlyData> yearlyData,
            int minYr, int xdiff, int ydiff)
        {
            int devYear = minYr + xdiff + ydiff;
            int originYear = minYr + ydiff;

            if (yearlyData == null)
                return 0m;
            else
                return (decimal)yearlyData
                    .Where(p => p.DevelopmentYear == devYear)
                    .Where(p => p.OriginYear == originYear)
                    .Select(p => p.IncrementalValue)
                    .FirstOrDefault();
        }

        public string FormattedNumber(decimal number)
        {
            var decimalAsString = number.ToString();

            return decimalAsString.Replace(".0", "");
        }
    }
}
