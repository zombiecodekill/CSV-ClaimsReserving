using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Correction
{
    public class MissingYears
    {
        public List<List<YearlyData>> Add(List<List<YearlyData>> recordsPerProduct, MinAndMaxYears minAndMaxYears)
        {
            /* The full data set is needed we before we can write out all the running totals for each product
             Wherever a year is not already specified we assume the Incremental Value is 0
            */
            foreach (var productRecords in recordsPerProduct)
            {
                var yearlyData = productRecords;
                AddForProduct(yearlyData, minAndMaxYears);
            }

            return recordsPerProduct;
        }

        public List<YearlyData> AddForProduct(List<YearlyData> records, MinAndMaxYears minAndMaxYears)
        {
            string productName = records.Select(r => r.ProductName).First();

            for (var i = minAndMaxYears.Minimum; i <= minAndMaxYears.Maximum; i++)
            {
                if (records.All(r => r.OriginYear != i))
                {
                    for (int j = i; j <= minAndMaxYears.Maximum; j++)
                    {
                        records.Add(new YearlyData { ProductName = productName, DevelopmentYear = j, OriginYear = i, IncrementalValue = 0});
                    }
                }
                else
                {
                    for (int j = i; j <= minAndMaxYears.Maximum; j++)
                    {
                        if (!records.Any(r => r.DevelopmentYear == j && r.OriginYear == i))
                        {
                            records.Add(new YearlyData { ProductName = productName, DevelopmentYear = j, OriginYear = i, IncrementalValue = 0 });
                        }
                    }
                }
            }

            return records;
        }

    }
}
