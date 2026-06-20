using Microsoft.AspNetCore.Mvc;

namespace HomeWard.Api.Controllers;

public class DocumentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
