FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["todolistapi.csproj", "."]
RUN dotnet restore "todolistapi.csproj"
COPY . .
RUN dotnet build "todolistapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "todolistapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todolistapi.dll"]