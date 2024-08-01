using Livraria.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Livraria.Domain.Tests
{
    public class AutorUnitTest
    {
        [Fact]
        public void CriarAutor_ComParametrosValidos_DeveCriarAutorComEstadoValido()
        {
            Action action = () => new Autor("Nome do Autor");
            action.Should().NotThrow<Livraria.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CriarAutor_ComIdNegativo_DeveLancarExcecaoDeIdInvalido()
        {
            Action action = () => new Autor(-1, "Nome do Autor");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Valor de Id inválido.");
        }

        [Fact]
        public void CriarAutor_ComNomeCurto_DeveLancarExcecaoDeNomeCurto()
        {
            Action action = () => new Autor("Ab");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido, muito curto, mínimo de 3 caracteres.");
        }

        [Fact]
        public void CriarAutor_ComNomeNulo_DeveLancarExcecaoDeNomeInvalido()
        {
            Action action = () => new Autor(null);
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido. O nome é obrigatório.");
        }

        [Fact]
        public void CriarAutor_ComNomeVazio_DeveLancarExcecaoDeNomeInvalido()
        {
            Action action = () => new Autor("");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido. O nome é obrigatório.");
        }

        [Fact]
        public void AtualizarNome_ComNomeValido_DeveAtualizarNome()
        {
            var autor = new Autor("Nome Antigo");
            autor.Update("Novo Nome");
            autor.Nome.Should().Be("Novo Nome");
        }

        [Fact]
        public void AtualizarNome_ComNomeCurto_DeveLancarExcecaoDeNomeCurto()
        {
            var autor = new Autor("Nome Antigo");
            Action action = () => autor.Update("Ab");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido, muito curto, mínimo de 3 caracteres.");
        }

        [Fact]
        public void AtualizarNome_ComNomeNulo_DeveLancarExcecaoDeNomeInvalido()
        {
            var autor = new Autor("Nome Antigo");
            Action action = () => autor.Update(null);
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido. O nome é obrigatório.");
        }

        [Fact]
        public void AtualizarNome_ComNomeVazio_DeveLancarExcecaoDeNomeInvalido()
        {
            var autor = new Autor("Nome Antigo");
            Action action = () => autor.Update("");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Nome inválido. O nome é obrigatório.");
        }
    }
}

