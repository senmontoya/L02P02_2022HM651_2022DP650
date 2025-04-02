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

        public IActionResult Index()
        {
            var autores = _context.Autores.ToList();
            return View(autores);
        }

        public IActionResult LibrosPorAutor(int idAutor)
        {
            var autor = _context.Autores.Find(idAutor);
            if (autor == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32("idAutorSeleccionado", idAutor);
            HttpContext.Session.SetString("nombreAutorSeleccionado", autor.autor);

            return RedirectToAction("Index", "Libros", new { idAutor = idAutor });
        }

        public IActionResult Volver()
        {
            return RedirectToAction("Index");
        }
    }
}
