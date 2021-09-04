#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MasterReport.UI/MasterReport.UI.csproj", "src/MasterReport.UI/"]
RUN dotnet restore "src/MasterReport.UI/MasterReport.UI.csproj"
COPY . .
WORKDIR "/src/src/MasterReport.UI"
RUN dotnet build "MasterReport.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterReport.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterReport.UI.dll"]