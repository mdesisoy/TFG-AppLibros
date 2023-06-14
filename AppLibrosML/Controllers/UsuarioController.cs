using System.Security.Claims;
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

        public ActionResult UsuarioPerfil()
        {
            string correoUsuario = HttpContext.Session.GetString("usuario");
            ViewBag.CorreoUsuario = correoUsuario;

            return View();
        }
    }
}
