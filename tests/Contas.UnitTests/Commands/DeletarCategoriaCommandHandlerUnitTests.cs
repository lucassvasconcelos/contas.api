using System;
using System.Threading;
using System.Threading.Tasks;
using Contas.Commands;
using Contas.Commands.Abstractions;
using Contas.Domain;
using CoreBox;
using CoreBox.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Contas.UnitTests.Commands
{
    public class DeletarCategoriaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public DeletarCategoriaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Deletar_Uma_Categoria(
            DeletarCategoriaCommand command,
            Categoria categoriaDb
        )
        {
            var commandHandler = new DeletarCategoriaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Id.Value)).ReturnsAsync(categoriaDb);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().DeleteAsync(categoriaDb)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            await commandHandler.Handle(command, default(CancellationToken));

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(command.Id.Value), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().DeleteAsync(categoriaDb), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Categoria_Nao_Foi_Encontrada(DeletarCategoriaCommand command)
        {
            var commandHandler = new DeletarCategoriaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Id.Value)).ReturnsAsync((Categoria)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Categoria informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().DeleteAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Id_Invalida(DeletarCategoriaCommand command)
        {
            command.Id = null;
            var commandHandler = new DeletarCategoriaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Id não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Categoria>().DeleteAsync(It.IsAny<Categoria>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}