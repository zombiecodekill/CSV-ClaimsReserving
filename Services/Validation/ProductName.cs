using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Validation
{
    public class ProductName
    {
        public Defects FindDifferentCasing(Defects defects, List<YearlyData> records)
        {
            var distinctCaseSensitiveProductNames = GetAllDistinctCaseSensitiveProductNames(records);

            SortedSet<string> lowerCasedProductNames = new SortedSet<string>();
            foreach (var product in distinctCaseSensitiveProductNames)
            {
                lowerCasedProductNames.Add(product.ToLower());
            }

            if (lowerCasedProductNames.Count < distinctCaseSensitiveProductNames.Length)
            {
                int num = distinctCaseSensitiveProductNames.Length - lowerCasedProductNames.Count;
                defects.Warnings.Add("Found " + num + " cases where one record product name is the same name but with different case to another record product name.");
            }

            return defects;
        }

        public string[] GetAllDistinctCaseSensitiveProductNames(List<YearlyData> records)
        {
            return records.Select(r => r.ProductName).Distinct().ToArray();
        }

    }
}
