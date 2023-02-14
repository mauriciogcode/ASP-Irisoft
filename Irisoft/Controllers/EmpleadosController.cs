using Irisoft.Models;
using Irisoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Irisoft.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IRepositorioEmpleado repositorioEmpleado;

        public EmpleadosController(IRepositorioEmpleado repositorioEmpleado)
        {
            this.repositorioEmpleado = repositorioEmpleado;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var listadoempleado = await repositorioEmpleado.ObtenerTodos();
            return View(listadoempleado);
        }

        public IActionResult Crear()
        {
            return View();
        }

        #endregion

        #region Crear

        [HttpPost]
        public async Task<IActionResult> Crear (Empleado empleado)
        {
            if(!ModelState.IsValid)
            {
                return View(empleado);
            }


            var yaExisteNombreEmpleado = await repositorioEmpleado.Existe(empleado.EmpleadoNombre);

            if (yaExisteNombreEmpleado)
            {
                ModelState.AddModelError(nameof(empleado.EmpleadoNombre), $"El nombre {empleado.EmpleadoNombre} ya existe");
                return View(empleado);
            }


            await repositorioEmpleado.Crear(empleado);
            return RedirectToAction("Index");
        }

        #endregion

        #region Editar

        [HttpGet]
        public async Task<IActionResult> Editar (int id)
        {
            var empleado = await repositorioEmpleado.ObtenerEntidadPorId(id);
            if (empleado is null) 
            {
                return RedirectToAction("NoEncontrado", "Home");

            }

            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar (Empleado empleado)
        {
            var empleadoExiste = await repositorioEmpleado.ObtenerEntidadPorId(empleado.Id);
            if (empleadoExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioEmpleado.Actualizar(empleado);
            return RedirectToAction("Index");
        }

        #endregion

        #region Borrar

        [HttpGet]
        public async Task<IActionResult> Borrar (int id)
        {
            var empleado = await repositorioEmpleado.ObtenerEntidadPorId(id);
            if (empleado is null)  return RedirectToAction("NoEncontrado", "Home");
            return View(empleado);
                 
        }

        [HttpPost]
        public async Task<IActionResult> BorrarEmpleado (Empleado empleado)
        {
            var empleadoExiste = await repositorioEmpleado.ObtenerEntidadPorId(empleado.Id);
            if (empleadoExiste is null) return RedirectToAction("NoEncontrado", "Home");
            await repositorioEmpleado.Borrar(empleado.Id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
