using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities
{
    public class Assunto : Entity
    {

        public Assunto() { }
        public string Descricao { get; private set; }

        public Assunto(string descricao)
        {
            ValidateDomain(descricao);
        }

        public Assunto(int id, string descricao)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id inválido.");
            Id = id;
            ValidateDomain(descricao);
        }

        public void Update(string descricao)
        {
            ValidateDomain(descricao);
        }

        private void ValidateDomain(string descricao)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(descricao),
                "Descrição inválida. A descrição é obrigatória.");

            DomainExceptionValidation.When(descricao.Length < 3,
                "Descrição inválida, muito curta, mínimo de 3 caracteres.");

            Descricao = descricao;
        }

        public ICollection<LivroAssunto> LivroAssuntos { get; set; }
    }
}

