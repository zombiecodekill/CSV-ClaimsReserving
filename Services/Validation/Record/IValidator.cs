using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Validation.Record
{
    public interface IValidator
    {
        Defects Validate(YearlyData yearlyData);
    }
}
