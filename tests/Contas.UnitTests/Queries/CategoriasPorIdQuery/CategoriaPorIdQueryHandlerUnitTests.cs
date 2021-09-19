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
    public class CategoriaPorIdQueryHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public CategoriaPorIdQueryHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Simular_A_Query_De_CategoriaPorId(CategoriaPorIdQuery request, DbConnection dbConnection)
        {
            var queryHandler = new CategoriaPorIdQueryHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetDbConnection()).Returns(dbConnection);
            await queryHandler.Handle(request, default(CancellationToken));
        }
    }
}