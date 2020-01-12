using System.Collections.Generic;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Writer
{
    public interface ICsvOutput
    {
        void Write(List<List<YearlyData>> recordsPerProduct, string outputFileName);

        string GetOutputFileName(string inputFileName);
    }
}