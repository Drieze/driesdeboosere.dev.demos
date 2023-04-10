FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
#COPY *.sln .
#COPY src/*.csproj ./src/
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
#COPY src/. ./src/
COPY . .
#WORKDIR /source/src
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
#EXPOSE 80
#EXPOSE 443
ENTRYPOINT ["dotnet", "DigitalOceanAppDeployment.Mvc.dll"]