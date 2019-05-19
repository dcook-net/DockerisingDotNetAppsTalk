#Set the base image on which to base this image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2.204

#Set the working directory for RUN, CMD, ENTRYPOINT, COPY and ADD instructions beyond this point
WORKDIR /app

#Copy files/folders from our filesystem to the container filesystem
COPY . . 

#Executes the publish command, outputting the result the the /app folder in the container
#NB: as of .Net Core v2.0 dotnet restore and dotnet build are called implicitly 
RUN dotnet publish -c Release ./src/IsPrime/IsPrime.csproj -o /app

#Sets the command to be executed when running the image
CMD ["dotnet", "IsPrime.dll"]