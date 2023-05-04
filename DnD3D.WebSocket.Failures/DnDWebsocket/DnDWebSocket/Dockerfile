FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DnDWebSocket/DnDWebSocket.csproj", "DnDWebSocket/"]
RUN dotnet restore "DnDWebSocket/DnDWebSocket.csproj"
COPY . .
WORKDIR "/src/DnDWebSocket"
RUN dotnet build "DnDWebSocket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DnDWebSocket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DnDWebSocket.dll"]
