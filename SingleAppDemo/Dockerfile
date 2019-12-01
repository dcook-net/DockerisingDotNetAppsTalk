FROM mcr.microsoft.com/dotnet/core/sdk:3.0.101-alpine3.10 AS builder
COPY . .

RUN dotnet test -c Release ./test/IsPrime.Tests/IsPrime.Tests.csproj
RUN dotnet publish -c Release ./src/IsPrime/IsPrime.csproj -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.1-alpine3.10

WORKDIR /app

COPY --from=builder /app .

CMD ["dotnet", "IsPrime.dll"]