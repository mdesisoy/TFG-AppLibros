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

        //CREO EL LISTADO DE LIBROS QUE SE VA A VER CAMBIAAAAAAAAAAAAR
        // GET: LibroController
        //public ActionResult Index()
        //{
        //    var listaLibros = contexto.Libros.ToList();
        //    return View(listaLibros);
        //}

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



    }
}
