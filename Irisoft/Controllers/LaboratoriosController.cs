using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Irisoft.Controllers
{
    public class LaboratoriosController : Controller
    {
        private readonly IRepositorioLaboratorio repositorioLaboratorio;

        public LaboratoriosController(IRepositorioLaboratorio repositorioLaboratorio)
        {
            this.repositorioLaboratorio = repositorioLaboratorio;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            var listadoLaboratorios = await repositorioLaboratorio.ObtenerTodos();
            return View(listadoLaboratorios);
        }
        public IActionResult Crear()
        {
            return View();
        }
        #endregion

        #region Crear
        [HttpPost]
        public async Task<IActionResult> Crear (Laboratorio laboratorio)
        {
            if (!ModelState.IsValid)
            {
                return View(laboratorio);
            }

            var yaExisteNombreLaboratorio = await repositorioLaboratorio.Existe(laboratorio.Nombre);

            if (yaExisteNombreLaboratorio)
            {
                ModelState.AddModelError(nameof(laboratorio.Nombre), $"El nombre {laboratorio.Nombre} ya existe");
                return View();
            }

            await repositorioLaboratorio.Crear(laboratorio);
            return RedirectToAction("Index");
        }
        #endregion

        #region Editar

        [HttpGet]
        public async Task<IActionResult> Editar (int id)
        {
            var laboratorio = await repositorioLaboratorio.ObtenerEntidadPorId(id);

            if (laboratorio is null) return RedirectToAction("NoEncontrado", "Home");

            return View(laboratorio);
        }

        [HttpPost]
        public async Task<IActionResult> Editar (Laboratorio laboratorio)
        {
            var laboratorioExiste = await repositorioLaboratorio.ObtenerEntidadPorId(laboratorio.Id);
            if (laboratorioExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioLaboratorio.Actualizar(laboratorio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Borrar
        [HttpGet]
        public async Task<IActionResult> Borrar (int id)
        {
            var laboratorio = await repositorioLaboratorio.ObtenerEntidadPorId(id);
            if (laboratorio is null) return RedirectToAction("NoEncontrado", "Home");
            return View(laboratorio);
        }

        [HttpPost]
           public async Task<IActionResult> BorrarLaboratorio (Laboratorio laboratorio)
        {
            var laboratorioExiste = await repositorioLaboratorio.ObtenerEntidadPorId(laboratorio.Id);
            if (laboratorioExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioLaboratorio.Borrar(laboratorio.Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Verificacion Front

        [HttpGet]
        public async Task<IActionResult> VerificarExisteLaboratorio(string nombre)
        {
            var yaExisteLaboratorio = await repositorioLaboratorio.Existe(nombre);

            if (yaExisteLaboratorio)
            {
                Json($"El nombre {nombre} ya existe");
            }
            
            return Json(true);
        }

        #endregion

    }
}
