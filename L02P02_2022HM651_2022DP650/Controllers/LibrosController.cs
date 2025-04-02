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

    // Existing Index action (Prototype 02)
    public IActionResult Index(int idAutor)
    {
        var autor = _context.Autores.Find(idAutor);
        if (autor == null)
        {
            return NotFound();
        }

        HttpContext.Session.SetInt32("idAutorSeleccionado", idAutor);
        HttpContext.Session.SetString("nombreAutorSeleccionado", autor.autor);

        var libros = _context.Libros
            .Where(l => l.id_autor == idAutor)
            .Include(l => l.autor)
            .ToList();

        return View(libros);
    }

    // Acción para mostrar los comentarios de un libro seleccionado (Prototype 03)
    public IActionResult Comentarios(int idLibro)
    {
        var libro = _context.Libros
            .Include(l => l.autor)
            .FirstOrDefault(l => l.Id == idLibro);
        if (libro == null)
        {
            return NotFound();
        }

        HttpContext.Session.SetInt32("idLibroSeleccionado", idLibro);
        HttpContext.Session.SetString("nombreLibroSeleccionado", libro.nombre);

        var comentarios = _context.Comentarios
            .Where(c => c.idLibro == idLibro)
            .ToList();

        ViewBag.Libro = libro;
        return View(comentarios);
    }
    // Acción para guardar un nuevo comentario
    
    [HttpPost]
    public IActionResult GuardarComentario(string comentarios, string usuario = "Anónimo")
    {
        var idLibro = HttpContext.Session.GetInt32("idLibroSeleccionado");
        if (idLibro == null || string.IsNullOrEmpty(comentarios))
        {
            return BadRequest("Datos inválidos.");
        }

        try
        {
            // Calcular el próximo Id manualmente
            int nuevoId = _context.Comentarios.Any() ? _context.Comentarios.Max(c => c.Id) + 1 : 1;

            var nuevoComentario = new Comentarios
            {
                Id = nuevoId, // Asignar el Id manualmente
                idLibro = idLibro.Value,
                comentarios = comentarios,
                usuario = usuario,
                createdAt = DateTime.Now
            };

            _context.Comentarios.Add(nuevoComentario);
            _context.SaveChanges();

            return RedirectToAction("ConfirmacionComentario", new { idComentario = nuevoComentario.Id });
        }
        catch (Exception ex)
        {
            var libro = _context.Libros
                .Include(l => l.autor)
                .FirstOrDefault(l => l.Id == idLibro);
            var listaComentarios = _context.Comentarios
                .Where(c => c.idLibro == idLibro)
                .ToList();

            ViewBag.Libro = libro;
            ViewBag.ErrorMessage = "Error al guardar el comentario: " + ex.Message;
            return View("Comentarios", listaComentarios); // Asegura que se pasa una lista
        }
    }

    [HttpGet]
    public IActionResult ConfirmacionComentario(int idComentario)
    {
        var comentario = _context.Comentarios
            .FirstOrDefault(c => c.Id == idComentario);
        if (comentario == null)
        {
            return RedirectToAction("Index", new { idAutor = HttpContext.Session.GetInt32("idAutorSeleccionado") });
        }

        var idLibro = HttpContext.Session.GetInt32("idLibroSeleccionado");
        var nombreLibro = HttpContext.Session.GetString("nombreLibroSeleccionado");
        var nombreAutor = HttpContext.Session.GetString("nombreAutorSeleccionado");

        // Asegurar idAutorSeleccionado
        var idAutorSeleccionado = HttpContext.Session.GetInt32("idAutorSeleccionado");
        if (!idAutorSeleccionado.HasValue && idLibro.HasValue)
        {
            var libro = _context.Libros
                .Include(l => l.autor)
                .FirstOrDefault(l => l.Id == idLibro.Value);
            if (libro != null)
            {
                idAutorSeleccionado = libro.id_autor;
                HttpContext.Session.SetInt32("idAutorSeleccionado", idAutorSeleccionado.Value);
            }
        }

        // Si nombreAutor es null, intentar recuperarlo
        if (string.IsNullOrEmpty(nombreAutor) && idLibro.HasValue)
        {
            var libro = _context.Libros
                .Include(l => l.autor)
                .FirstOrDefault(l => l.Id == idLibro.Value);
            if (libro != null)
            {
                nombreAutor = libro.autor?.autor;
                HttpContext.Session.SetString("nombreAutorSeleccionado", nombreAutor ?? "Desconocido");
            }
        }

        ViewBag.NombreLibro = nombreLibro;
        ViewBag.NombreAutor = nombreAutor ?? "Autor no disponible";
        return View("~/Views/Libros/ConfirmacionComentario.cshtml", comentario);
    }
}