using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities
{
    public class Livro : Entity
    {
        public Livro() { }

        public string Titulo { get; private set; }
        public string Editora { get; private set; }
        public int Edicao { get; private set; }
        public string AnoPublicacao { get; private set; }

        public Livro(string titulo, string editora, int edicao, string anoPublicacao)
        {
            ValidarDominio(titulo, editora, edicao, anoPublicacao);
        }

        public Livro(int id, string titulo, string editora, int edicao, string anoPublicacao)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id inválido.");
            Id = id;
            ValidarDominio(titulo, editora, edicao, anoPublicacao);
        }

        public void Update(string titulo, string editora, int edicao, string anoPublicacao)
        {
            ValidarDominio(titulo, editora, edicao, anoPublicacao);
        }

        private void ValidarDominio(string titulo, string editora, int edicao, string anoPublicacao)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(titulo),
                "Título inválido. O título é obrigatório.");

            DomainExceptionValidation.When(titulo.Length < 3,
                "Título inválido, muito curto, mínimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(editora),
                "Editora inválida. A editora é obrigatória.");

            DomainExceptionValidation.When(edicao < 1, "Valor de edição inválido.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(anoPublicacao),
                "Ano de publicação inválido. O ano de publicação é obrigatório.");

            DomainExceptionValidation.When(anoPublicacao.Length != 4,
                "Ano de publicação inválido, deve ter 4 caracteres.");

            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }

        public ICollection<LivroAutor> LivroAutores { get; set; }
        public ICollection<LivroAssunto> LivroAssuntos { get; set; }
        public ICollection<LivroPrecoCanalVenda> LivroPrecoCanalVenda { get; set; }
    }
}


