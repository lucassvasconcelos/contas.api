dotnet cake
dotnet reportgenerator -reports:tests/Contas.UnitTests/.coverage/cov.cobertura.xml -targetdir:"coveragereport/unit" -reporttypes:Html
dotnet reportgenerator -reports:tests/Contas.IntegrationTests/.coverage/cov.cobertura.xml -targetdir:"coveragereport/intg" -reporttypes:Html