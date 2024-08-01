using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities
{
    public class Autor : Entity
    {
        public Autor() { }
        public string Nome { get; private set; }

        public Autor(string nome)
        {
            ValidateDomain(nome);
        }

        public Autor(int id, string nome)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id inválido.");
            Id = id;
            ValidateDomain(nome);
        }

        public void Update(string nome)
        {
            ValidateDomain(nome);
        }

        private void ValidateDomain(string nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome),
                "Nome inválido. O nome é obrigatório.");

            DomainExceptionValidation.When(nome.Length < 3,
                "Nome inválido, muito curto, mínimo de 3 caracteres.");

            Nome = nome;
        }

        public ICollection<LivroAutor> LivroAutores { get; set; }
    }
}


