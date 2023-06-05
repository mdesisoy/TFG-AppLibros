using Microsoft.EntityFrameworkCore;
using AppLibrosML.Models;

namespace AppLibrosML.Models
{
    public class Contexto : DbContext //emula una base de datos y crea tabla
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) //inyección de dependencias
        {

        }
        public DbSet<UsuarioModelo> Usuarios { get; set; } //tabla para los usuarios
        public DbSet<LibroModelo> Libros { get; set; } //tabla para los libros

    }
}
