namespace Livraria.Domain.Entities
{
    public class LivroAutor 
    {
        public LivroAutor() { }
        public int Livro_Codl { get; set; }
        public int Autor_CodAu { get; set; }

        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}

