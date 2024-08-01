using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities
{
    public class CanalVenda : Entity
    {
        public CanalVenda() { }

        public string NomeCanal { get; private set; }

        public CanalVenda(string nomeCanal)
        {
            ValidarDominio(nomeCanal);
        }

        public CanalVenda(int id, string nomeCanal)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id inválido.");
            Id = id;
            ValidarDominio(nomeCanal);
        }

        public void Update(string nomeCanal)
        {
            ValidarDominio(nomeCanal);
        }

        private void ValidarDominio(string nomeCanal)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nomeCanal),
                "Nome do canal é obrigatório.");
            DomainExceptionValidation.When(nomeCanal.Length < 3,
                "Nome do canal inválido, muito curto, mínimo de 3 caracteres.");

            NomeCanal = nomeCanal;
        }

        public ICollection<LivroPrecoCanalVenda> LivroPrecoCanalVenda { get; set; }
    }
}

