using Microsoft.AspNetCore.Mvc;

namespace HomeWard.Api.Controllers;

public class SessionTransitionController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
