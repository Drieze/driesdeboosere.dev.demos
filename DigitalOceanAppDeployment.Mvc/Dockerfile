#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

RUN ls
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
RUN ls
COPY *.csproj .
RUN ls
RUN dotnet restore
RUN ls
COPY . .
RUN ls

RUN dotnet publish -c release -o /app --no-restore

RUN ls
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
RUN ls
COPY --from=build /app/ ./
RUN ls
ENTRYPOINT ["dotnet", "DigitalOceanAppDeployment.Mvc.dll"]