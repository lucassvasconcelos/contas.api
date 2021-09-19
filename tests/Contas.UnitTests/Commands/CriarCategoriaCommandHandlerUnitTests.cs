using System;
using System.Threading;
using System.Threading.Tasks;
using Contas.Commands;
using Contas.Commands.Abstractions;
using Contas.Domain;
using Contas.UnitTests.Domain.Categorias;
using CoreBox;
using CoreBox.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Contas.UnitTests.Commands
{
    public class CriarCategoriaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public CriarCategoriaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Criar_Uma_Categoria(
            CriarCategoriaCommand command
        )
        {
            var commandHandler = new CriarCategoriaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            var categoria = await commandHandler.Handle(command, default(CancellationToken));
            categoria.CategoriaCriadaShouldBeValid();

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(categoria), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Nome_Invalida(CriarCategoriaCommand command)
        {
            command.Nome = string.Empty;
            var commandHandler = new CriarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Nome não informado");

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Descricao_Invalida(CriarCategoriaCommand command)
        {
            command.Descricao = string.Empty;
            var commandHandler = new CriarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Descrição não informada");

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Tipo_Invalida(CriarCategoriaCommand command)
        {
            command.Tipo = null;
            var commandHandler = new CriarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Tipo não informado");

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Usuario_Invalida(CriarCategoriaCommand command)
        {
            command.Usuario = null;
            var commandHandler = new CriarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Usuário não informado");

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}