using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services;
using ClaimsReserving.Services.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using ClaimsReserving.Services.Correction;
using ClaimsReserving.Services.Writer;
using IncrementalValue = ClaimsReserving.Services.Correction.IncrementalValue;

namespace ClaimsReserving.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ICsvLoader _csvLoader;
        private readonly IDirectoryFinder _directoryFinder;
        private readonly ICsvOutput _csvOutput;
        private readonly IFileValidator _fileValidator;
        private readonly IRecordsPerProduct _recordsPerProduct;
        private readonly string _inputFilesPath;

        public ClaimsController(ICsvLoader csvLoader, IDirectoryFinder directoryFinder, IConfiguration configuration, 
            ICsvOutput csvOutput, IFileValidator fileValidator, IRecordsPerProduct recordsPerProduct)
        {
            _directoryFinder = directoryFinder;
            _csvLoader = csvLoader;
            _csvOutput = csvOutput;
            _fileValidator = fileValidator;
            _recordsPerProduct = recordsPerProduct;
            _inputFilesPath = configuration.GetValue<string>("InputFilesDirectory");
        }

        public IActionResult Index()
        {
            var model = new CsvModel {FilesFoundMessage = _csvLoader.FindFilesMessage()};

            if (_directoryFinder.Exists(_inputFilesPath))
            {
                model.Files = _csvLoader.GetFiles();
            }

            return View(model);
        }

        public IActionResult ProcessFile(int id)
        {
            /* 
             * This solution assumes that this web application is running on an intranet (not the public internet) and none of the users are malicious
             * There is basic protection insofar as this method does not accept any string inputs
             * However if this were to go on the public internet, a security review might first be needed
             */
            var file = _csvLoader.GetFileById(id);

            if (file == null)
            {
                throw new System.Exception("File Not Found");
            }

            var records = _csvLoader.LoadCsvFile(_inputFilesPath + "\\" + file.Name);
            IncrementalValue.DefaultNullsToZeroes(records);

            var defects = _fileValidator.Validate(file, records);

            
            if (defects != null && defects.Errors.Any())
            {
                return RedirectToAction("Index", "Failure", defects);
            }

            if (records != null)
            {
                MinAndMaxYears minAndMaxYears = new MinAndMaxYears
                {
                    Minimum = records.Min(p => p.OriginYear),
                    Maximum = records.Max(p => p.OriginYear)
                };

                var recordsPerProduct = _recordsPerProduct.SortRecordsByProduct(records);

                
                recordsPerProduct = new MissingYears().Add(recordsPerProduct, minAndMaxYears);

                string outputFileName = _csvOutput.GetOutputFileName(file.Name);
                _csvOutput.Write(recordsPerProduct, outputFileName);
            }


            return RedirectToAction("Index","Success", new { id = file.Id, fileName = file.Name });
        }



    }
}