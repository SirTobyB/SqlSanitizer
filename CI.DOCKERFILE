FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app

COPY ./api .
COPY ./ui/* ./wwwroot/ 

EXPOSE 5000
ENTRYPOINT ["dotnet", "SqlSanitizer.Api.dll"]