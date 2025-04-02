﻿using L02P02_2022HM651_2022DP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class LibrosController : Controller
{
    private readonly LibreriaContext _context;

    public LibrosController(LibreriaContext context)
    {
        _context = context;
    }

    public IActionResult Index(int idAutor)
    {
        var autor = _context.Autores.Find(idAutor);
        if (autor == null)
        {
            return NotFound();
        }

        // Guardar el autor seleccionado en la sesión
        HttpContext.Session.SetInt32("idAutorSeleccionado", idAutor);
        HttpContext.Session.SetString("nombreAutorSeleccionado", autor.autor);

        // Obtener los libros del autor seleccionado
        var libros = _context.Libros
            .Where(l => l.id_autor == idAutor)
            .Include(l => l.autor)
            .ToList();

        return View(libros);
    }

    // Acción para mostrar los comentarios de un libro
    
}