using ClaimsReserving.Models;
using ClaimsReserving.Services;
using ClaimsReserving.Services.Writer;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsReserving.Controllers
{
    public class SuccessController : Controller
    {
        private readonly ICsvLoader _csvLoader;
        private readonly ICsvOutput _csvOutput;
        public SuccessController(ICsvLoader csvLoader, ICsvOutput csvOutput)
        {
            _csvLoader = csvLoader;
            _csvOutput = csvOutput;
        }

        public IActionResult Index(int id)
        {
            var file = _csvLoader.GetFileById(id);

            string outputFileName = _csvOutput.GetOutputFileName(file.Name);

            return View(new SuccessModel()
            {
                Id = file.Id,
                Name = file.Name,
                OutputFileName = outputFileName
            });
        }
    }
}