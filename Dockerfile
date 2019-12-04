# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR ./
COPY *.sln .
COPY *.csproj .
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR ./
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR ./
COPY --from=publish /publish .

# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FVSystem.dll