using System.Collections.Generic;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Correction
{
    public static class IncrementalValue
    {
        public static List<YearlyData> DefaultNullsToZeroes(List<YearlyData> records)
        {
            if (records == null) return null;

            foreach (var record in records)
            {
                if (record.IncrementalValue == null) record.IncrementalValue = 0;
            }

            return records;
        }

    }
}
