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

        // GET: LibroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
