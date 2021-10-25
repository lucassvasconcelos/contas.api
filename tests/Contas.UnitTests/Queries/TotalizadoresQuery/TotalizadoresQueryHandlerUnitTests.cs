using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using CoreBox.Repositories;
using Moq;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class TotalizadoresQueryHandlerUnitTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public TotalizadoresQueryHandlerUnitTests()
            => _mockUnitOfWork = new Mock<IUnitOfWork>();

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Simular_A_Query_De_Totalizadores(TotalizadoresQuery request, DbConnection dbConnection)
        {
            var queryHandler = new TotalizadoresQueryHandler(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(s => s.GetDbConnection()).Returns(dbConnection);
            await queryHandler.Handle(request, default(CancellationToken));
        }
    }
}