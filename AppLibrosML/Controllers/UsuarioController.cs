using AppLibrosML.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLibrosML.Controllers
{
    public class UsuarioController : Controller
    {
        public Contexto contexto { get; } //private readonly Contexto contexto; 

        public UsuarioController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModelo usuario)
        {
            try
            {
                contexto.Usuarios.Add(usuario);
                contexto.Database.EnsureCreated(); //mapeado en la base de datos, esta instruccion securiza
                contexto.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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
