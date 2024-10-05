using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Usuario;

namespace P1700_DSM.Controllers
{
    public class UsuarioController : Controller
    {
		public IActionResult InicioSesion()
		{
			return View(new InicioSesionDto());
		}
	}
}
