FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS dotnet-builder
WORKDIR /app

COPY SqlSanitizer.Api/ ./

RUN dotnet restore --source https://www.myget.org/F/appmetrics/api/v3/index.json --source https://api.nuget.org/v3/index.json
RUN dotnet publish -c Release -o /out SqlSanitizer.Api.csproj

FROM node:10-slim as ng-builder
WORKDIR /app

COPY sql-sanitizer/ ./
ENV PATH /app/src/app/node_modules/.bin:$PATH

RUN yarn global add @angular/cli
RUN yarn
RUN ng build --prod --source-map=false

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine
WORKDIR /app

COPY --from=dotnet-builder /out .
COPY --from=ng-builder /app/dist/sql-sanitizer/* ./wwwroot/

EXPOSE 5000
ENTRYPOINT ["dotnet", "SqlSanitizer.Api.dll"]
