#Build Environment
FROM microsoft/dotnet:2.1.403-sdk as build-env 
WORKDIR app

COPY Century.sln ./
COPY NuGet.Config ./
COPY src/Century.Api/Century.Api.csproj src/Century.Api/
COPY tests/Century.Tests/Century.Tests.csproj tests/Century.Tests/

RUN dotnet restore

COPY . ./
RUN dotnet publish --configuration Release -o ./out
