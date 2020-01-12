using System;
using ClaimsReserving.Models;

namespace ClaimsReserving.Services.Validation.Record
{
    public class YearValidator : IValidator
    {
        private readonly Defects _defects;

        public YearValidator(Defects defects)
        {
            _defects = defects;
        }

        
        public int WatsonAndSonsYearFounded = 1878;

        public Defects Validate(YearlyData yearlyData)
        {
            if (yearlyData.DevelopmentYear < yearlyData.OriginYear)
            {
                _defects.Errors.Add("The Development Year " + yearlyData.DevelopmentYear +
                                    " is less than the Origin Year " + yearlyData.OriginYear);
            }
            if (yearlyData.OriginYear < WatsonAndSonsYearFounded)
            {
                _defects.Errors.Add("The Origin Year " + yearlyData.OriginYear + " is too old");
            }
            if (yearlyData.DevelopmentYear < WatsonAndSonsYearFounded)
            {
                _defects.Errors.Add("The Development Year " + yearlyData.DevelopmentYear + " is too old");
            }

            if (yearlyData.OriginYear > DateTime.Now.Year)
            {
                _defects.Warnings.Add("The Origin Year " + yearlyData.OriginYear +
                                      " post dates the year that this PC’s system clock is set to (" + DateTime.Now.Year +
                                      ").");
            }

            return _defects;
        }
    }
}