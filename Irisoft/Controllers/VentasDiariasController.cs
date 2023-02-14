using Irisoft.Models;
using Irisoft.Services;
using Microsoft.AspNetCore.Mvc;

namespace Irisoft.Controllers
{
    public class VentasDiariasController : Controller
    {
        private readonly IVentaDiariaRepositorio ventaDiariaRepositorio;

        public VentasDiariasController(IVentaDiariaRepositorio ventaDiariaRepositorio)
        {
            this.ventaDiariaRepositorio = ventaDiariaRepositorio;
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(VentaDiaria ventadiaria)

        {
            if(!ModelState.IsValid)
            {
                return View(ventadiaria);
            }

            ventaDiariaRepositorio.Crear(ventadiaria);

            return View();
        }
    }
}
