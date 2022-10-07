#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dotnet9.Web/Dotnet9.Web.csproj", "Dotnet9.Web/"]
COPY ["Dotnet9.Application/Dotnet9.Application.csproj", "Dotnet9.Application/"]
COPY ["Dotnet9.Domain/Dotnet9.Domain.csproj", "Dotnet9.Domain/"]
COPY ["Dotnet9.Domain.Shared/Dotnet9.Domain.Shared.csproj", "Dotnet9.Domain.Shared/"]
COPY ["Dotnet9.Core/Dotnet9.Core.csproj", "Dotnet9.Core/"]
COPY ["Dotnet9.Application.Contracts/Dotnet9.Application.Contracts.csproj", "Dotnet9.Application.Contracts/"]
COPY ["Dotnet9.EntityFrameworkCore/Dotnet9.EntityFrameworkCore.csproj", "Dotnet9.EntityFrameworkCore/"]
RUN dotnet restore "Dotnet9.Web/Dotnet9.Web.csproj"
COPY . .
WORKDIR "/src/Dotnet9.Web"
RUN dotnet build "Dotnet9.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet9.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet9.Web.dll"]