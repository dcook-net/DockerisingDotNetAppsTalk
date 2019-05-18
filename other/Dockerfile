FROM mcr.microsoft.com/dotnet/core/sdk:2.2.204-alpine3.9 AS builder
COPY . .

RUN dotnet test -c Release 
RUN dotnet publish -c Release ./src/MeetupMembers/MeetupMembers.csproj -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.5-alpine3.9
WORKDIR /app

COPY --from=builder /app .

EXPOSE 9010
CMD ["dotnet", "MeetupMembers.dll"]