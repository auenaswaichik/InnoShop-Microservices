FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /app
COPY . .
WORKDIR /app/WebApi
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build app/publish .
ENTRYPOINT ["dotnet","WebApi.dll"]
