using System.Collections.Generic;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services
{
    public interface IRecordsPerProduct
    {
        List<List<YearlyData>> SortRecordsByProduct(List<YearlyData> records);
    }
}