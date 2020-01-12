using System.Collections.Generic;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Validation
{
    public interface IFileValidator
    {
        Defects Validate(FileModel file, List<YearlyData> records);
    }
}