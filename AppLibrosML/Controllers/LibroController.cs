using System.Drawing.Printing;
using System.Security.Claims;
using AppLibrosML.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppLibrosML.Controllers
{
    public class LibroController : Controller
    {
        public Contexto contexto { get; } //private readonly Contexto contexto; 

        public LibroController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        // Método para obtener el ID del usuario actual
        private int GetUsuarioId()
        {
            return (int)HttpContext.Session.GetInt32("UsuarioId");
        }

        //PARA QUE EL USUARIO CREE EL LIBRO
        // GET: MarcaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarcaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibroModelo libro)
        {
            try
            {
                int usuarioId = GetUsuarioId();

                using (var transaction = contexto.Database.BeginTransaction())
                {
                    try
                    {
                        contexto.Libros.Add(libro);
                        contexto.SaveChanges();

                        LibrosUsuarioModelo librosUsuario = new LibrosUsuarioModelo()
                        {
                            usuarioID = usuarioId,
                            libroID = libro.ID
                        };

                        contexto.LibrosUsuario.Add(librosUsuario);
                        contexto.SaveChanges();

                        transaction.Commit();

                        return RedirectToAction("UsuarioPerfil", "UsuarioLogin");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; // Puedes manejar o personalizar la excepción aquí si es necesario
                    }
                }
            }
            catch
            {
                return View("Create");
            }
        }     

        //Sacar listado de libros según el usuario
        public ActionResult Listado()
        {
            int usuarioId = GetUsuarioId();

            List<LibroModelo> librosUsuario = contexto.LibrosUsuario
                .Where(lu => lu.usuarioID == usuarioId)
                .Select(lu => new LibroModelo
                {
                    ID = lu.libro.ID,
                    Titulo = lu.libro.Titulo,
                    Autor = lu.libro.Autor,
                    Editorial = lu.libro.Editorial ?? string.Empty,
                    Genero = lu.libro.Genero,
                    Estado = lu.libro.Estado,
                    Comentarios = lu.libro.Comentarios ?? string.Empty,
                    Valoracion = lu.libro.Valoracion
                })
                .ToList();

            return View(librosUsuario);
        }

        // GET: Libro/Edit/5
        public ActionResult Edit(int id)
        {
            var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);

            if (libro == null)
            {
                return NotFound();
            }

            LibroModelo libroModelo = new LibroModelo //Este modelo es el que se mostrará en el edit y sobre el que se edita
            {
                ID = libro.ID,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Editorial = libro.Editorial ?? string.Empty,
                Genero = libro.Genero,
                Estado = libro.Estado,
                Comentarios = libro.Comentarios ?? string.Empty,
                Valoracion = libro.Valoracion
            };

            return View(libroModelo);
        }

        // POST: Libro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LibroModelo libroActualizado)
        {
            if (id != libroActualizado.ID)
            {
                return NotFound();
            }

            var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);

            if (libro == null)
            {
                return NotFound();
            }

            libro.Titulo = libroActualizado.Titulo;
            libro.Autor = libroActualizado.Autor;
            libro.Editorial = libroActualizado.Editorial ?? string.Empty;
            libro.Genero = libroActualizado.Genero;
            libro.Estado = libroActualizado.Estado;
            libro.Comentarios = libroActualizado.Comentarios ?? string.Empty;
            libro.Valoracion = libroActualizado.Valoracion;

            contexto.SaveChanges();

            return RedirectToAction("Listado");
        }

        //GET: Libro/Delete
        public ActionResult Delete(int id)
        {
            var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);

            if (libro == null)
            {
                return NotFound();
            }

            LibroModelo libroModelo = new LibroModelo
            {
                ID = libro.ID,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Editorial = libro.Editorial ?? string.Empty,
                Genero = libro.Genero,
                Estado = libro.Estado,
                Comentarios = libro.Comentarios ?? string.Empty,
                Valoracion = libro.Valoracion
            };

            return View(libroModelo);
        }

        //POST: Libro/Delete
        [HttpPost]
        public ActionResult DeleteConfirmed(int id) //PARA CONFIRMAR EN LA VISTA QUE QUIERES BORRAR
        {
            var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);

            if (libro == null)
            {
                return NotFound();
            }

            contexto.Libros.Remove(libro); //elimina de la base de datos
            contexto.SaveChanges();

            return RedirectToAction("Listado");
        }

    }
}
