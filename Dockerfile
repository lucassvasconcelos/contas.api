FROM mcr.microsoft.com/dotnet/sdk:5.0-focal as build

EXPOSE 80

WORKDIR /app

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

COPY /nuget.config /app/nuget.config
COPY /Contas.API.sln /app/Contas.API.sln
COPY /src/Contas.API/Contas.API.csproj /app/src/Contas.API/Contas.API.csproj
COPY /src/Contas.Commands/Contas.Commands.csproj /app/src/Contas.Commands/Contas.Commands.csproj
COPY /src/Contas.Commands.Abstractions/Contas.Commands.Abstractions.csproj /app/src/Contas.Commands.Abstractions/Contas.Commands.Abstractions.csproj
COPY /src/Contas.Domain/Contas.Domain.csproj /app/src/Contas.Domain/Contas.Domain.csproj
COPY /src/Contas.DomainServices/Contas.DomainServices.csproj /app/src/Contas.DomainServices/Contas.DomainServices.csproj
COPY /src/Contas.DomainServices.Abstractions/Contas.DomainServices.Abstractions.csproj /app/src/Contas.DomainServices.Abstractions/Contas.DomainServices.Abstractions.csproj
COPY /src/Contas.Events/Contas.Events.csproj /app/src/Contas.Events/Contas.Events.csproj
COPY /src/Contas.Events.Abstractions/Contas.Events.Abstractions.csproj /app/src/Contas.Events.Abstractions/Contas.Events.Abstractions.csproj
COPY /src/Contas.Infra.Repositories/Contas.Infra.Repositories.csproj /app/src/Contas.Infra.Repositories/Contas.Infra.Repositories.csproj
COPY /src/Contas.Infra.Repositories.Abstractions/Contas.Infra.Repositories.Abstractions.csproj /app/src/Contas.Infra.Repositories.Abstractions/Contas.Infra.Repositories.Abstractions.csproj
COPY /src/Contas.Queries/Contas.Queries.csproj /app/src/Contas.Queries/Contas.Queries.csproj
COPY /src/Contas.Queries.Abstractions/Contas.Queries.Abstractions.csproj /app/src/Contas.Queries.Abstractions/Contas.Queries.Abstractions.csproj
COPY /tests/Contas.UnitTests/Contas.UnitTests.csproj /app/tests/Contas.UnitTests/Contas.UnitTests.csproj
COPY /tests/Contas.IntegrationTests/Contas.IntegrationTests.csproj /app/tests/Contas.IntegrationTests/Contas.IntegrationTests.csproj

RUN dotnet restore --no-cache

COPY . .

RUN dotnet build && dotnet publish -c Release -o /build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal

WORKDIR /app

COPY --from=build /build ./

ENTRYPOINT ["dotnet", "./Contas.API.dll"]
