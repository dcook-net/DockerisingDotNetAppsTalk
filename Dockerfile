FROM mcr.microsoft.com/dotnet/core/sdk:2.2.203

WORKDIR /app

COPY . . 

RUN dotnet publish -c Release ./src/IsPrime/IsPrime.csproj -o /app

EXPOSE 9021

CMD ["dotnet", "IsPrime.dll"]