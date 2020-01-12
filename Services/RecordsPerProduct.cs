using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation;

namespace ClaimsReserving.Services
{
    public class RecordsPerProduct : IRecordsPerProduct
    {
        public List<List<YearlyData>> SortRecordsByProduct(List<YearlyData> records)
        {
            var products = new ProductName().GetAllDistinctCaseSensitiveProductNames(records);

            var recordsPerProduct = new List<List<YearlyData>>();

            foreach (var product in products)
            {
                var productRecords = records.Where(r => r.ProductName == product).ToList();
                recordsPerProduct.Add(productRecords);
            }

            return recordsPerProduct;
        }

    }
}
