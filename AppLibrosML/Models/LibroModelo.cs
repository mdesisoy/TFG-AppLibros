using System.ComponentModel.DataAnnotations;

namespace AppLibrosML.Models
{
    public class LibroModelo
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo Título es obligatorio.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo Autor es obligatorio.")]
        public string Autor { get; set; }
        public string Editorial { get; set; }
        [Required(ErrorMessage = "El campo Género es obligatorio.")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        public string Estado { get; set; }
        public string Comentarios { get; set; }
        public int? Valoracion { get; set; }


        //Para sacar listado relacionado
        public ICollection<LibrosUsuarioModelo> LibrosUsuarios { get; set; }
    }
}
