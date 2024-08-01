using Livraria.Domain.Validation;

namespace Livraria.Domain.Entities
{
    public class LivroPrecoCanalVenda
    {
        public LivroPrecoCanalVenda() { }

        public int Livro_Codl { get; private set; }
        public int CanalVenda_CodCanal { get; private set; }
        public decimal PrecoVenda { get; private set; }

        public LivroPrecoCanalVenda(int livro_Codl, int canalVenda_CodCanal, decimal precoVenda)
        {
            DomainExceptionValidation.When(livro_Codl <= 0, "Id do livro inválido.");
            DomainExceptionValidation.When(canalVenda_CodCanal <= 0, "Id do canal de venda inválido.");
            DomainExceptionValidation.When(precoVenda <= 0, "Preço de venda inválido. O preço deve ser maior que zero.");

            Livro_Codl = livro_Codl;
            CanalVenda_CodCanal = canalVenda_CodCanal;
            PrecoVenda = precoVenda;
        }

        public void Update(int livro_Codl, int canalVenda_CodCanal, decimal precoVenda)
        {
            DomainExceptionValidation.When(livro_Codl <= 0, "Id do livro inválido.");
            DomainExceptionValidation.When(canalVenda_CodCanal <= 0, "Id do canal de venda inválido.");
            DomainExceptionValidation.When(precoVenda <= 0, "Preço de venda inválido. O preço deve ser maior que zero.");

            Livro_Codl = livro_Codl;
            CanalVenda_CodCanal = canalVenda_CodCanal;
            PrecoVenda = precoVenda;
        }

        public Livro Livro { get; set; }
        public CanalVenda CanalVenda { get; set; }
    }
}

