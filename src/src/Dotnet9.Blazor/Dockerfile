#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Dotnet9.Blazor/Dotnet9.Blazor.csproj", "src/Dotnet9.Blazor/"]
COPY ["BasicTheme/Volo.Abp.AspNetCore.Components.Web.BasicTheme/Volo.Abp.AspNetCore.Components.Web.BasicTheme.csproj", "BasicTheme/Volo.Abp.AspNetCore.Components.Web.BasicTheme/"]
COPY ["src/Dotnet9.Application/Dotnet9.Application.csproj", "src/Dotnet9.Application/"]
COPY ["src/Dotnet9.Domain/Dotnet9.Domain.csproj", "src/Dotnet9.Domain/"]
COPY ["src/Dotnet9.Domain.Shared/Dotnet9.Domain.Shared.csproj", "src/Dotnet9.Domain.Shared/"]
COPY ["src/Dotnet9.Abp.BlobStoring.TencentCloud/Dotnet9.Abp.BlobStoring.TencentCloud.csproj", "src/Dotnet9.Abp.BlobStoring.TencentCloud/"]
COPY ["src/Dotnet9.Application.Contracts/Dotnet9.Application.Contracts.csproj", "src/Dotnet9.Application.Contracts/"]
COPY ["src/Dotnet9.HttpApi/Dotnet9.HttpApi.csproj", "src/Dotnet9.HttpApi/"]
COPY ["BasicTheme/Volo.Abp.AspNetCore.Components.Server.BasicTheme/Volo.Abp.AspNetCore.Components.Server.BasicTheme.csproj", "BasicTheme/Volo.Abp.AspNetCore.Components.Server.BasicTheme/"]
COPY ["src/Dotnet9.EntityFrameworkCore/Dotnet9.EntityFrameworkCore.csproj", "src/Dotnet9.EntityFrameworkCore/"]
RUN dotnet restore "src/Dotnet9.Blazor/Dotnet9.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Dotnet9.Blazor"
RUN dotnet build "Dotnet9.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet9.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet9.Blazor.dll"]