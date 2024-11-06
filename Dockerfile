FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIROMENT=Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /MarketplaceApi

COPY ["MarketplaceApi/MarketplaceApi.csproj", "MarketplaceApi/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "MarketplaceApi/MarketplaceApi.csproj"

COPY . .
FROM build as publish
RUN dotnet publish "MarketplaceApi/MarketplaceApi.csproj" -c Release -o /app/publish /p:UserAppHost=false

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketplaceApi.dll"]