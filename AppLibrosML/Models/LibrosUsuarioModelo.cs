namespace AppLibrosML.Models
{
    public class LibrosUsuarioModelo
    {
        public int ID { get; set; }
        public int usuarioID { get; set; } //para relacionaar con tabla usuarios
        public UsuarioModelo usuario { get; set; }
        public int libroID { get; set; }//para relacionaar con tabla llibros
        public LibroModelo libro { get; set; }
    }
}
