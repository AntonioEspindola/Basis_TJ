using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Domain.Entities
{
    [Table("vwLivroAutorAssunto")]
    public class LivroAutorAssunto
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public int AnoPublicacao { get; set; }
        public int AutorId { get; set; }
        public string AutorNome { get; set; }
        public int AssuntoId { get; set; }
        public string AssuntoDescricao { get; set; }
    }
}
