FROM mcr.microsoft.com/dotnet/core/sdk:2.2.204 AS builder
COPY . .
RUN dotnet publish -c Release ./src/IsPrime/IsPrime.csproj -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.5
WORKDIR /app

COPY --from=builder /app .

CMD ["dotnet", "IsPrime.dll"]