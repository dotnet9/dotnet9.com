#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8066
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Dotnet9.Web.Entry/Dotnet9.Web.Entry.csproj", "Dotnet9.Web.Entry/"]
COPY ["Dotnet9.Web.Core/Dotnet9.Web.Core.csproj", "Dotnet9.Web.Core/"]
COPY ["Dotnet9.Application/Dotnet9.Application.csproj", "Dotnet9.Application/"]
COPY ["Dotnet9.Core/Dotnet9.Core.csproj", "Dotnet9.Core/"]
RUN dotnet restore "Dotnet9.Web.Entry/Dotnet9.Web.Entry.csproj"
COPY . .
WORKDIR "/src/Dotnet9.Web.Entry"
RUN dotnet build "Dotnet9.Web.Entry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet9.Web.Entry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#�޸�apt-getԴ������apt����
RUN sed -i s@/deb.debian.org/@/mirrors.163.com/@g /etc/apt/sources.list
RUN cat /etc/apt/sources.list
#��װfontconfig
RUN apt-get clean
RUN apt-get update && apt-get install -y fontconfig

ENV ASPNETCORE_URLS 'http://*:8066'

ENTRYPOINT ["dotnet", "Dotnet9.Web.Entry.dll"]