using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using CoreBox.Repositories;
using Moq;
using Xunit;

namespace Contas.UnitTests.Queries.ContasPorIdQuery
{
    public class ContaPorIdQueryHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ContaPorIdQueryHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Simular_A_Query_De_ContaPorId(ContaPorIdQuery request, DbConnection dbConnection)
        {
            var queryHandler = new ContaPorIdQueryHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetDbConnection()).Returns(dbConnection);
            await queryHandler.Handle(request, default(CancellationToken));
        }
    }
}