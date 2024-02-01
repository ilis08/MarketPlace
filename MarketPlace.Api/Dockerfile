FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["ApplicationService/ApplicationService.csproj", "ApplicationService/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Exceptions/Exceptions.csproj", "Exceptions/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish 
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]