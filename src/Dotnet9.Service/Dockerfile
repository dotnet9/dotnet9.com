#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Directory.Build.Props", "."]
COPY ["Directory.Build.Targets", "."]
COPY ["Dotnet9.Service/Dotnet9.Service.csproj", "Dotnet9.Service/"]
COPY ["Dotnet9.ASPNETCore/Dotnet9.ASPNETCore.csproj", "Dotnet9.ASPNETCore/"]
COPY ["Dotnet9.Commons/Dotnet9.Commons.csproj", "Dotnet9.Commons/"]
COPY ["Dotnet9.Contracts/Dotnet9.Contracts.csproj", "Dotnet9.Contracts/"]
# RUN dotnet restore "Dotnet9.Service/Dotnet9.Service.csproj"
COPY . .
WORKDIR "/src/Dotnet9.Service"
RUN dotnet build "Dotnet9.Service.csproj" --os linux -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet9.Service.csproj" --os linux -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet9.Service.dll"]
