FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build-env
WORKDIR /app

COPY * ./

RUN dotnet publish -c Release -o /out SqlSanitizer.Api.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine
WORKDIR /app

COPY --from=build-env /out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "SqlSanitizer.Api.dll"]