using Microsoft.AspNetCore.Mvc;

namespace WebCalculator.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();
}
