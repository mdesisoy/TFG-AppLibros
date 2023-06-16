using System.ComponentModel.DataAnnotations;

namespace AppLibrosML.Models
{
    public class LibroModelo
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Genero { get; set; }
        public string Estado { get; set; }
        public string Comentarios { get; set; }
        public int? Valoracion { get; set; }


        //Para sacar listado relacionado
        public ICollection<LibrosUsuarioModelo> LibrosUsuarios { get; set; }
    }
}
