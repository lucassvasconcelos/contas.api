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
    public class DeletarContaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public DeletarContaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Deletar_Uma_Conta(
            DeletarContaCommand command,
            Conta contaDb
        )
        {
            var commandHandler = new DeletarContaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value)).ReturnsAsync(contaDb);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().DeleteAsync(contaDb)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            await commandHandler.Handle(command, default(CancellationToken));

            _mockUnitOfWork.Verify(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().DeleteAsync(contaDb), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Conta_Nao_Foi_Encontrada(DeletarContaCommand command)
        {
            var commandHandler = new DeletarContaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value)).ReturnsAsync((Conta)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Conta informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Conta>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().DeleteAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Id_Invalida(DeletarContaCommand command)
        {
            command.Id = null;
            var commandHandler = new DeletarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Id não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Conta>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().DeleteAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}