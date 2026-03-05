using Microsoft.AspNetCore.Mvc;
using MedicalCatalog.Web.Models;

namespace MedicalCatalog.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error(string? requestId)
    {
        return View(new ErrorViewModel { RequestId = requestId ?? HttpContext.TraceIdentifier });
    }
}
