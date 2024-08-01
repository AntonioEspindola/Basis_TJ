using Livraria.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Livraria.Domain.Tests
{
    public class LivroUnitTest
    {
        [Fact]
        public void CriarLivro_ComParametrosValidos_DeveCriarLivroComEstadoValido()
        {
            Action action = () => new Livro("Título do Livro", "Editora do Livro", 1, "2023");
            action.Should().NotThrow<Livraria.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CriarLivro_ComIdNegativo_DeveLancarExcecaoDeIdInvalido()
        {
            Action action = () => new Livro(-1, "Título do Livro", "Editora do Livro", 1, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Valor de Id inválido.");
        }

        [Fact]
        public void CriarLivro_ComTituloCurto_DeveLancarExcecaoDeTituloCurto()
        {
            Action action = () => new Livro("Ti", "Editora do Livro", 1, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Título inválido, muito curto, mínimo de 3 caracteres.");
        }

        [Fact]
        public void CriarLivro_ComTituloNulo_DeveLancarExcecaoDeTituloInvalido()
        {
            Action action = () => new Livro(null, "Editora do Livro", 1, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Título inválido. O título é obrigatório.");
        }

        [Fact]
        public void CriarLivro_ComEditoraNula_DeveLancarExcecaoDeEditoraInvalida()
        {
            Action action = () => new Livro("Título do Livro", null, 1, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Editora inválida. A editora é obrigatória.");
        }

        [Fact]
        public void AtualizarLivro_ComParametrosValidos_DeveAtualizarLivro()
        {
            var livro = new Livro("Título Antigo", "Editora Antiga", 1, "2022");
            livro.Update("Novo Título", "Nova Editora", 2, "2023");
            livro.Titulo.Should().Be("Novo Título");
            livro.Editora.Should().Be("Nova Editora");
            livro.Edicao.Should().Be(2);
            livro.AnoPublicacao.Should().Be("2023");
        }

        [Fact]
        public void AtualizarLivro_ComEditoraNula_DeveLancarExcecaoDeEditoraInvalida()
        {
            var livro = new Livro("Título Antigo", "Editora Antiga", 1, "2022");
            Action action = () => livro.Update("Novo Título", null, 2, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Editora inválida. A editora é obrigatória.");
        }

        [Fact]
        public void AtualizarLivro_ComEdicaoInvalida_DeveLancarExcecaoDeEdicaoInvalida()
        {
            var livro = new Livro("Título Antigo", "Editora Antiga", 1, "2022");
            Action action = () => livro.Update("Novo Título", "Nova Editora", 0, "2023");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Valor de edição inválido.");
        }

        [Fact]
        public void AtualizarLivro_ComAnoPublicacaoInvalido_DeveLancarExcecaoDeAnoPublicacaoInvalido()
        {
            var livro = new Livro("Título Antigo", "Editora Antiga", 1, "2022");
            Action action = () => livro.Update("Novo Título", "Nova Editora", 2, "23");
            action.Should().Throw<Livraria.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Ano de publicação inválido, deve ter 4 caracteres.");
        }
    }
}

