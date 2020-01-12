using System.Collections.Generic;
using ClaimsReserving.Models;
using CsvHelper;

namespace ClaimsReserving.Services.Writer
{
    public interface ISummaryRecord
    {
        void Write(CsvWriter csv, List<YearlyData> records);
    }
}