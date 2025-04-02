using Microsoft.AspNetCore.Mvc;
using L02P02_2022HM651_2022DP650.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace L02P02_2022HM651_2022DP650.Controllers
{
    public class AutoresController : Controller
    {
        private readonly LibreriaContext _context;

        public AutoresController(LibreriaContext context)
        {
            _context = context;
        }

        // Acción para mostrar el listado de autores
        public IActionResult Index()
        {
            var autores = _context.Autores.ToList();
            return View(autores);
        }

        // Acción para mostrar el listado de libros de un autor
        public IActionResult LibrosPorAutor(int idAutor)
        {
            var autor = _context.Autores.Find(idAutor);
            if (autor == null)
            {
                return NotFound();
            }

            // Guardar el id del autor en la sesión
            HttpContext.Session.SetInt32("idAutorSeleccionado", idAutor);
            HttpContext.Session.SetString("nombreAutorSeleccionado", autor.autor);

            // Redirigir a la acción Index en el controlador Libros, pasando el idAutor
            return RedirectToAction("Index", "Libros", new { idAutor = idAutor });
        }



        // Acción para manejar la vuelta al listado de autores
        public IActionResult Volver()
        {
            return RedirectToAction("Index");
        }
    }
}
