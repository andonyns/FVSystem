# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR 
COPY *.sln .
COPY *.csproj FVSystem/
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR /FVSystem
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FVSystem.dll