using ClaimsReserving.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsReserving.Controllers
{
    public class FailureController : Controller
    {
        public IActionResult Index(Defects defects)
        {
            return View(defects);
        }
    }
}