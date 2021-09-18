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
    public class AtualizarContaCommandHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AtualizarContaCommandHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Atualizar_Uma_Conta(
            AtualizarContaCommand command,
            Conta contaDb,
            Categoria categoria
        )
        {
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value)).ReturnsAsync(contaDb);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value)).ReturnsAsync(categoria);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().UpdateAsync(It.IsAny<Conta>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.CommitAsync()).Returns(Task.CompletedTask);

            var conta = await commandHandler.Handle(command, default(CancellationToken));
            conta.ContaAtualizadaDeveSerValida();

            conta.Nome.Should().Be(command.Nome);
            conta.Data.Should().Be(command.Data.Value);
            conta.Valor.Should().Be(command.Valor.Value);
            conta.Parcelado.Should().Be(command.Parcelado.Value);
            conta.NumeroParcelas.Should().Be(command.NumeroParcelas.Value);
            conta.Observacao.Should().Be(command.Observacao);
            conta.Usuario.Should().Be(command.Usuario.Value);
            conta.IdCategoria.Should().Be(categoria.Id);
            conta.Categoria.Should().Be(null);

            _mockUnitOfWork.Verify(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value), Times.Once);
            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().UpdateAsync(conta), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Conta_Nao_Foi_Encontrada(AtualizarContaCommand command)
        {
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value)).ReturnsAsync((Conta)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Conta informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Conta>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().UpdateAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Informar_Que_A_Categoria_Nao_Foi_Encontrada(AtualizarContaCommand command, Conta conta)
        {
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetRepository<Conta>().GetByIdAsync(command.Id.Value)).ReturnsAsync(conta);
            _mockUnitOfWork.Setup(s => s.GetRepository<Categoria>().GetByIdAsync(command.Categoria.Value)).ReturnsAsync((Categoria)null);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("A Categoria informada não foi encontrada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Id_Invalida(AtualizarContaCommand command)
        {
            command.Id = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Id não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Nome_Invalida(AtualizarContaCommand command)
        {
            command.Nome = string.Empty;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Nome não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Data_Invalida(AtualizarContaCommand command)
        {
            command.Data = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Data da conta não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Valor_Invalida(AtualizarContaCommand command)
        {
            command.Valor = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

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
        public void Deve_Alertar_Sobre_A_Propriedade_Parcelado_Invalida(AtualizarContaCommand command)
        {
            command.Parcelado = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Flag Parcelado não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_NumeroParcelas_Invalida(AtualizarContaCommand command)
        {
            command.Parcelado = true;
            command.NumeroParcelas = 0;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

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
        public void Deve_Alertar_Sobre_A_Propriedade_Categoria_Invalida(AtualizarContaCommand command)
        {
            command.Categoria = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Categoria não informada");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Alertar_Sobre_A_Propriedade_Usuario_Invalida(AtualizarContaCommand command)
        {
            command.Usuario = null;
            var commandHandler = new AtualizarContaCommandHandler(_mockUnitOfWork.Object);

            Func<Task> act = async () => { await commandHandler.Handle(command, default(CancellationToken)); };
            act.Should().ThrowAsync<ValidationException>("Usuário não informado");

            _mockUnitOfWork.Verify(s => s.GetRepository<Categoria>().GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.GetRepository<Conta>().SaveAsync(It.IsAny<Conta>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitAsync(), Times.Never);
        }
    }
}