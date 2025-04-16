// Ejemplo de controlador para la ruta raíz
using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Content("La aplicación está funcionando correctamente.");
    }
}
