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
                contexto.Libros.Add(libro);
                contexto.Database.EnsureCreated();
                contexto.SaveChanges();
                return RedirectToAction("UsuarioPerfil", "UsuarioLogin");
            }
            catch
            {
                return View("Create");
            }
        }

        //CREO EL LISTADO DE LIBROS QUE SE VA A VER CAMBIAAAAAAAAAAAAR
        // GET: LibroController
        public ActionResult Index()
        {
            var listaLibros = contexto.Libros.ToList();
            return View(listaLibros);
        }

    }
}
