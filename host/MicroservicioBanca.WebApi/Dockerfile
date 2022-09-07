#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["host/MicroservicioBanca.WebApi/MicroservicioBanca.WebApi.csproj", "host/MicroservicioBanca.WebApi/"]
COPY ["src/MicroservicioBanca.Dependencies/MicroservicioBanca.Dependencies.csproj", "src/MicroservicioBanca.Dependencies/"]
COPY ["src/MicroservicioBanca.Repository/MicroservicioBanca.Repository.csproj", "src/MicroservicioBanca.Repository/"]
COPY ["src/MicroservicioBanca.Domain/MicroservicioBanca.Domain.csproj", "src/MicroservicioBanca.Domain/"]
COPY ["src/MicroservicioBanca.Domain.Shared/MicroservicioBanca.Domain.Shared.csproj", "src/MicroservicioBanca.Domain.Shared/"]
COPY ["src/MicroservicioBanca.Application/MicroservicioBanca.Application.csproj", "src/MicroservicioBanca.Application/"]
COPY ["src/MicroservicioBanca.Application.Contracts/MicroservicioBanca.Application.Contracts.csproj", "src/MicroservicioBanca.Application.Contracts/"]
RUN dotnet restore "host/MicroservicioBanca.WebApi/MicroservicioBanca.WebApi.csproj"
COPY . .
WORKDIR "/src/host/MicroservicioBanca.WebApi"
RUN dotnet build "MicroservicioBanca.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroservicioBanca.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroservicioBanca.WebApi.dll"]