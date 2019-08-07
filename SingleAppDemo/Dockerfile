FROM mcr.microsoft.com/dotnet/core/sdk:2.2.401-alpine3.9 AS builder
COPY . .

RUN dotnet test -c Release ./test/IsPrime.Tests/IsPrime.Tests.csproj
RUN dotnet publish -c Release ./src/IsPrime/IsPrime.csproj -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9

WORKDIR /app

COPY --from=builder /app .

CMD ["dotnet", "IsPrime.dll"]