using System.Collections.Generic;
using System.Linq;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation.Record;

namespace ClaimsReserving.Services.Validation
{
    public class FileValidator : IFileValidator
    {
        public Defects Validate(FileModel file, List<YearlyData> records)
        {
            Defects defects = new Defects { FileName = file.Name };
            if (records == null || !records.Any())
            {
                defects.Errors.Add("No data found in file.");
                return defects;
            }

            YearValidator yearValidator = new YearValidator(defects);
            foreach (var record in records)
            {
                defects = yearValidator.Validate(record);
            }

            ProductNameValidator productNameValidator = new ProductNameValidator(defects);
            foreach (var record in records)
            {
                defects = productNameValidator.Validate(record);
            }

            defects = new ProductName().FindDifferentCasing(defects, records);

            return defects;
        }
    }
}
