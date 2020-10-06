FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /app

COPY SqlSanitizer.Api/ ./

RUN dotnet restore
RUN dotnet publish -c Release -o /out SqlSanitizer.Api.csproj

FROM node:14.11 as ng-builder
WORKDIR /app

RUN npm install -g @ionic/cli

COPY ion-sanitizer/package.json ./package.json
COPY ion-sanitizer/package-lock.json ./package-lock.json

RUN npm ci

COPY ion-sanitizer/ ./
RUN ionic build --prod

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=dotnet-builder /out .
COPY --from=ng-builder /app/www/* ./wwwroot/

EXPOSE 5000
ENTRYPOINT ["dotnet", "SqlSanitizer.Api.dll"]
