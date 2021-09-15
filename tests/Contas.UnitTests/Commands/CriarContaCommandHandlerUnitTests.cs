using System;
using System.Threading;
using System.Threading.Tasks;
using Contas.Commands;
using Contas.Commands.Abstractions;
using Contas.Domain;
using Contas.UnitTests.Domain.Contas;
using CoreBox;
using CoreBox.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Contas.UnitTests.Commands
{
    public class CriarContaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public CriarContaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Criar_Uma_Conta(
            CriarContaCommand command,
            Categoria categoria
        )
        {
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value)).ReturnsAsync(categoria);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            var conta = await commandHandler.Handle(command, default(CancellationToken));
            conta.ShouldBeValid();

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(conta), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Categoria_Nao_Foi_Encontrada(CriarContaCommand command)
        {
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value)).ReturnsAsync((Categoria)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Categoria informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Nome_Invalida(CriarContaCommand command)
        {
            command.Nome = string.Empty;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Nome não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Data_Invalida(CriarContaCommand command)
        {
            command.Data = null;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Data da conta não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Valor_Invalida(CriarContaCommand command)
        {
            command.Valor = null;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Valor não informado");

            command.Valor = 0;

            Func<Task> act2 = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act2.Should().ThrowAsync<ValidationException>("Valor não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Parcelado_Invalida(CriarContaCommand command)
        {
            command.Parcelado = null;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Flag Parcelado não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_NumeroParcelas_Invalida(CriarContaCommand command)
        {
            command.Parcelado = true;
            command.NumeroParcelas = 0;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Número de parcelas não informado");

            command.Parcelado = true;
            command.NumeroParcelas = null;

            Func<Task> act2 = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act2.Should().ThrowAsync<ValidationException>("Número de parcelas não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Categoria_Invalida(CriarContaCommand command)
        {
            command.Categoria = null;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Categoria não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Usuario_Invalida(CriarContaCommand command)
        {
            command.Usuario = null;
            var commandHandler = new CriarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Usuário não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}