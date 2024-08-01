using Livraria.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Livraria.Domain.Tests
{
    public class AssuntoUnitTest
    {
        [Fact]
        public void CriarAssunto_ComDescricaoValida_DeveCriarAssuntoComEstadoValido()
        {
            // Arrange & Act
            Action action = () => new Assunto("Descrição do Assunto");

            // Assert
            action.Should().NotThrow<Livraria.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CriarAssunto_ComIdNegativo_DeveLancarExcecaoDeIdInvalido()
        {
            // Arrange & Act
            Action action = () => new Assunto(-1, "Descrição do Assunto");

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Valor de Id inválido.");
        }

        [Fact]
        public void CriarAssunto_ComDescricaoCurta_DeveLancarExcecaoDeDescricaoCurta()
        {
            // Arrange & Act
            Action action = () => new Assunto("Ab");

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Descrição inválida, muito curta, mínimo de 3 caracteres.");
        }

        [Fact]
        public void CriarAssunto_ComDescricaoNula_DeveLancarExcecaoDeDescricaoNula()
        {
            // Arrange & Act
            Action action = () => new Assunto(null);

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Descrição inválida. A descrição é obrigatória.");
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoValida_DeveAtualizarDescricao()
        {
            // Arrange
            var assunto = new Assunto("Descrição Antiga");

            // Act
            assunto.Update("Nova Descrição");

            // Assert
            assunto.Descricao.Should().Be("Nova Descrição");
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoCurta_DeveLancarExcecaoDeDescricaoCurta()
        {
            // Arrange
            var assunto = new Assunto("Descrição Antiga");

            // Act
            Action action = () => assunto.Update("Ab");

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Descrição inválida, muito curta, mínimo de 3 caracteres.");
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoNula_DeveLancarExcecaoDeDescricaoNula()
        {
            // Arrange
            var assunto = new Assunto("Descrição Antiga");

            // Act
            Action action = () => assunto.Update(null);

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Descrição inválida. A descrição é obrigatória.");
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoVazia_DeveLancarExcecaoDeDescricaoVazia()
        {
            // Arrange
            var assunto = new Assunto("Descrição Antiga");

            // Act
            Action action = () => assunto.Update("");

            // Assert
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Descrição inválida. A descrição é obrigatória.");
        }
    }
}



