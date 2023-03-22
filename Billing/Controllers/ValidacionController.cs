using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers;

[Route ("VALIDACION/[controller]")]
[ApiController]

public class ValidacionController : Controller
{

    [HttpGet]

    public IActionResult Index()
    {
        return View();
    }
}
