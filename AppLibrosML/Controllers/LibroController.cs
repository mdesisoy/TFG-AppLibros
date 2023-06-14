using AppLibrosML.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLibrosML.Controllers
{
    public class LibroController : Controller
    {
        public Contexto contexto { get; } //private readonly Contexto contexto; 

        public LibroController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        //CREO EL LISTADO DE LIBROS QUE SE VA A VER
        // GET: LibroController
        public ActionResult Index()
        {
            var listaLibros = contexto.Libros.ToList();
            return View(listaLibros);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            LibroModelo libro = contexto.Libros.FirstOrDefault(v => v.ID == id);
            return View(libro);
        }
    }
}
