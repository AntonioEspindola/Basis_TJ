using AutoMapper;
using FluentAssertions;
using Livraria.Application.DTOs;
using Livraria.Application.Interfaces;
using Livraria.Application.Services;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Livraria.Application.Tests
{
    public class AutorServiceTests
    {
        private readonly Mock<IAutorRepository> _autorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AutorService _autorService;

        public AutorServiceTests()
        {
            _autorRepositoryMock = new Mock<IAutorRepository>();
            _mapperMock = new Mock<IMapper>();
            _autorService = new AutorService(_autorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAutores_DeveRetornarListaDeAutores()
        {
            // Arrange
            var autorEntities = new List<Autor>
            {
                new Autor("Autor 1"),
                new Autor("Autor 2")
            };
            var autorDtos = new List<AutorDTO>
            {
                new AutorDTO { Nome = "Autor 1" },
                new AutorDTO { Nome = "Autor 2" }
            };

            _autorRepositoryMock.Setup(repo => repo.GetAutoresAsync())
                .ReturnsAsync(autorEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AutorDTO>>(autorEntities))
                .Returns(autorDtos);

            // Act
            var result = await _autorService.GetAutores();

            // Assert
            result.Should().BeEquivalentTo(autorDtos);
        }

        [Fact]
        public async Task GetById_DeveRetornarAutorPorId()
        {
            //// Arrange
            //var autorEntity = new Autor("Autor 1") { Id = 1 };
            //var autorDto = new AutorDTO { Id = 1, Nome = "Autor 1" };

            //_autorRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
            //    .ReturnsAsync(autorEntity);
            //_mapperMock.Setup(mapper => mapper.Map<AutorDTO>(autorEntity))
            //    .Returns(autorDto);

            //// Act
            //var result = await _autorService.GetById(1);

            //// Assert
            //result.Should().BeEquivalentTo(autorDto);
        }

        [Fact]
        public async Task Add_DeveAdicionarAutor()
        {
            // Arrange
            var autorDto = new AutorDTO { Nome = "Novo Autor" };
            var autorEntity = new Autor("Novo Autor");

            _mapperMock.Setup(mapper => mapper.Map<Autor>(autorDto))
                .Returns(autorEntity);
            _autorRepositoryMock.Setup(repo => repo.CreateAsync(autorEntity))
                .ReturnsAsync(autorEntity);
            _mapperMock.Setup(mapper => mapper.Map<AutorDTO>(autorEntity))
                .Returns(autorDto);

            // Act
            await _autorService.Add(autorDto);

            // Assert
            autorDto.Id.Should().Be(autorEntity.Id);
            _autorRepositoryMock.Verify(repo => repo.CreateAsync(autorEntity), Times.Once);
        }

        [Fact]
        public async Task Update_DeveAtualizarAutor()
        {
            //// Arrange
            //var autorDto = new AutorDTO { Id = 1, Nome = "Autor Atualizado" };
            //var autorEntity = new Autor("Autor Atualizado") { Id = 1 };

            //_mapperMock.Setup(mapper => mapper.Map<Autor>(autorDto))
            //    .Returns(autorEntity);
            //_autorRepositoryMock.Setup(repo => repo.UpdateAsync(autorEntity))
            //    .Returns(autorEntity);

            //// Act
            //await _autorService.Update(autorDto);

            //// Assert
            //_autorRepositoryMock.Verify(repo => repo.UpdateAsync(autorEntity), Times.Once);
        }

        [Fact]
        public async Task Remove_DeveRemoverAutor()
        {
            //// Arrange
            //var autorEntity = new Autor("Autor Para Remover") { Id = 1 };

            //_autorRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
            //    .ReturnsAsync(autorEntity);
            //_autorRepositoryMock.Setup(repo => repo.RemoveAsync(autorEntity))
            //    .Returns(Task.CompletedTask);

            //// Act
            //await _autorService.Remove(1);

            //// Assert
            //_autorRepositoryMock.Verify(repo => repo.RemoveAsync(autorEntity), Times.Once);
        }
    }
}

