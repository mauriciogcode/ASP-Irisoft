using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Irisoft.Controllers
{
    public class TarjetasController : Controller
    {
        private readonly IRepositorioTarjeta repositorioTarjeta;

        public TarjetasController(IRepositorioTarjeta repositorioTarjeta)
        {
            this.repositorioTarjeta = repositorioTarjeta;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            var listadoTarjetas = await repositorioTarjeta.ObtenerTodos();
            return View(listadoTarjetas);
        }
        public IActionResult Crear()
        {
            return View();
        }
        #endregion

        #region Crear
        [HttpPost]
        public async Task<IActionResult> Crear(Tarjeta Tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return View(Tarjeta);
            }

            var yaExisteNombreTarjeta = await repositorioTarjeta.Existe(Tarjeta.Nombre);

            if (yaExisteNombreTarjeta)
            {
                ModelState.AddModelError(nameof(Tarjeta.Nombre), $"El nombre {Tarjeta.Nombre} ya existe");
                return View();
            }

            await repositorioTarjeta.Crear(Tarjeta);
            return RedirectToAction("Index");
        }
        #endregion

        #region Editar

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var Tarjeta = await repositorioTarjeta.ObtenerEntidadPorId(id);

            if (Tarjeta is null) return RedirectToAction("NoEncontrado", "Home");

            return View(Tarjeta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Tarjeta Tarjeta)
        {
            var TarjetaExiste = await repositorioTarjeta.ObtenerEntidadPorId(Tarjeta.Id);
            if (TarjetaExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioTarjeta.Actualizar(Tarjeta);
            return RedirectToAction("Index");
        }

        #endregion

        #region Borrar
        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var Tarjeta = await repositorioTarjeta.ObtenerEntidadPorId(id);
            if (Tarjeta is null) return RedirectToAction("NoEncontrado", "Home");
            return View(Tarjeta);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarTarjeta(Tarjeta Tarjeta)
        {
            var TarjetaExiste = await repositorioTarjeta.ObtenerEntidadPorId(Tarjeta.Id);
            if (TarjetaExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioTarjeta.Borrar(Tarjeta.Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Verificacion Front

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTarjeta(string nombre)
        {
            var yaExisteTarjeta = await repositorioTarjeta.Existe(nombre);

            if (yaExisteTarjeta)
            {
                Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }

        #endregion

    }
}
