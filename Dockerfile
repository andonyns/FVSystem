# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY *.sln .
COPY FVSystem/*.csproj FVSystem/
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR /src/FVSystem
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FVSystem.dll