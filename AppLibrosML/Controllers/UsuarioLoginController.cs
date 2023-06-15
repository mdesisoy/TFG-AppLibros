using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using AppLibrosML.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AppLibrosML.Controllers
{
    public class UsuarioLoginController : Controller
    {
        static string cadena = "server=localhost; database=AppLibros; Trusted_Connection=true; MultipleActiveResultSets=true; Encrypt=False";
        public Contexto contexto { get; } //private readonly Contexto contexto; 

        public UsuarioLoginController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        //GET:Acceso
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult Registrar(UsuarioModelo usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Contrasenia == usuario.ConfirmarContrasenia)
            {

                usuario.Contrasenia = ConvertirSha256(usuario.Contrasenia);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden"; //Con viewdata enviamos datos del cotroller al view
                return View(); //volvemos a la vista
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                //llamar al procedimiento
                SqlCommand cmd = new SqlCommand("registrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", usuario.CorreoElectronico);
                cmd.Parameters.AddWithValue("Contrasenia", usuario.Contrasenia);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "UsuarioLogin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(UsuarioModelo usuario)
        {
            usuario.Contrasenia = ConvertirSha256(usuario.Contrasenia);

            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("validarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", usuario.CorreoElectronico);
                cmd.Parameters.AddWithValue("Contrasenia", usuario.Contrasenia);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                usuario.ID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (usuario.ID != 0)
            {
                //Session["usuario"] = usuario;
                HttpContext.Session.SetString("usuario", usuario.CorreoElectronico);
                return RedirectToAction("UsuarioPerfil", "UsuarioLogin");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
        }

        //ENCRIPTACION DE CONTRASEÑAS
        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public ActionResult UsuarioPerfil()
        {
            string correoUsuario = HttpContext.Session.GetString("usuario");
            ViewBag.CorreoUsuario = correoUsuario;

            return View();
        }
    }
}
