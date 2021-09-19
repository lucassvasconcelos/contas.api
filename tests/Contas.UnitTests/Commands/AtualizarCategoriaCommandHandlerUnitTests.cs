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
    public class AtualizarCategoriaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AtualizarCategoriaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Atualizar_Uma_Categoria(
            AtualizarCategoriaCommand command,
            Categoria categoriaDb
        )
        {
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Id.Value)).ReturnsAsync(categoriaDb);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().UpdateAsync(It.IsAny<Categoria>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            var categoria = await commandHandler.Handle(command, default(CancellationToken));
            categoria.CategoriaAtualizadaDeveSerValida();

            categoria.Nome.Should().Be(command.Nome);
            categoria.Descricao.Should().Be(command.Descricao);
            categoria.Tipo.Should().Be(command.Tipo);
            categoria.Usuario.Should().Be(command.Usuario.Value);

            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().GetByIdAsync(command.Id.Value), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().UpdateAsync(categoria), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Categoria_Nao_Foi_Encontrada(AtualizarCategoriaCommand command)
        {
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Id.Value)).ReturnsAsync((Categoria)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Categoria informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().UpdateAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Id_Invalida(AtualizarCategoriaCommand command)
        {
            command.Id = null;
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Id não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Nome_Invalida(AtualizarCategoriaCommand command)
        {
            command.Nome = string.Empty;
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Nome não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Descricao_Invalida(AtualizarCategoriaCommand command)
        {
            command.Descricao = string.Empty;
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Descrição não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Tipo_Invalida(AtualizarCategoriaCommand command)
        {
            command.Tipo = null;
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Tipo não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Usuario_Invalida(AtualizarCategoriaCommand command)
        {
            command.Usuario = null;
            var commandHandler = new AtualizarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Usuário não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().SaveAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}