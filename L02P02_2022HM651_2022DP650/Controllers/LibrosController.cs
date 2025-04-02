using L02P02_2022HM651_2022DP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class LibrosController : Controller
{
    private readonly LibreriaContext _context;

    public LibrosController(LibreriaContext context)
    {
        _context = context;
    }

    // Acción para mostrar los comentarios de un libro
    public IActionResult Index(int idLibro)
    {
        var libro = _context.Libros
            .Include(l => l.autor)
            .FirstOrDefault(l => l.Id == idLibro);  // Usar id_libro en lugar de idAutor

        if (libro == null)
        {
            return NotFound();
        }

        // Guardar el id del libro en la sesión
        HttpContext.Session.SetInt32("idLibroSeleccionado", idLibro);
        HttpContext.Session.SetString("tituloLibroSeleccionado", libro.nombre);  // Cambié 'titulo' por 'nombre'

        var comentarios = _context.Comentarios
            .Where(c => c.idLibro == idLibro)
            .Include(c => c.id_libro)
            .ToList();

        ViewBag.NombreAutor = libro.autor.autor;
        ViewBag.TituloLibro = libro.nombre;

        return View(comentarios);  // Mostrar los comentarios del libro seleccionado
    }

    // Acción para mostrar el formulario de nuevo comentario
    public IActionResult NuevoComentario()
    {
        var idLibro = HttpContext.Session.GetInt32("idLibroSeleccionado");
        if (idLibro == null)
        {
            return RedirectToAction("Index", "Autores");
        }

        ViewBag.NombreAutor = HttpContext.Session.GetString("NombreAutorSeleccionado");
        ViewBag.TituloLibro = HttpContext.Session.GetString("tituloLibroSeleccionado");

        var nuevoComentario = new Comentarios
        {
            idLibro = idLibro.Value,
            createdAt = DateTime.Now
        };

        return View(nuevoComentario);
    }

    // Acción para guardar un nuevo comentario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GuardarComentario(Comentarios comentario)
    {
        if (ModelState.IsValid)
        {
            comentario.createdAt = DateTime.Now;
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            return RedirectToAction("Index", new { idLibro = comentario.idLibro });
        }

        ViewBag.NombreAutor = HttpContext.Session.GetString("NombreAutorSeleccionado");
        ViewBag.TituloLibro = HttpContext.Session.GetString("tituloLibroSeleccionado");

        return View("NuevoComentario", comentario);
    }

    // Acción para volver a la lista de libros del autor
    public IActionResult VolverALibros()
    {
        var idAutor = HttpContext.Session.GetInt32("idAutorSeleccionado");
        if (idAutor == null)
        {
            return RedirectToAction("Index", "Autores");
        }

        return RedirectToAction("Index", "Autores", new { idAutor = idAutor });
    }
}