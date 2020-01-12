using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Validation.Record
{
    public class ProductNameValidator : IValidator
    {
        private readonly Defects _defects;
        public ProductNameValidator(Defects defects)
        {
            _defects = defects;
        }

        public Defects Validate(YearlyData yearlyData)
        {
            // Add any validation relevant rules relating to allowed product name values

            return _defects;
        }
    }
}
