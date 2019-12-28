# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /FVSystem/
COPY FVSystem/*.sln .
COPY FVSystem/*.csproj .
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR ./FVSystem/
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR ./FVSystem/app
COPY --from=publish /publish .

# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FVSystem.dll