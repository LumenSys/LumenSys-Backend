FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y libicu-dev
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LumenSys.WebAPI.csproj", "./"]
RUN dotnet restore "LumenSys.WebAPI.csproj"
COPY . .
RUN dotnet build "LumenSys.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LumenSys.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LumenSys.WebAPI.dll"]
