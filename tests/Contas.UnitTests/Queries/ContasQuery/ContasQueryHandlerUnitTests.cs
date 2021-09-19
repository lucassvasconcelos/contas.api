using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Contas.Domain;
using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using CoreBox.Repositories;
using Dapper;
using Moq;
using Xunit;

namespace Contas.UnitTests.Queries.ContasPorIdQuery
{
    public class ContasQueryHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ContasQueryHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Simular_A_Query_De_Contas(ContasQuery request, DbConnection dbConnection)
        {
            var queryHandler = new ContasQueryHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetDbConnection()).Returns(dbConnection);
            await queryHandler.Handle(request, default(CancellationToken));
        }
    }
}