FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Jx.Gateway/Jx.Gateway.csproj", "Jx.Gateway/"]
RUN dotnet restore "Jx.Gateway/Jx.Gateway.csproj"
COPY . .
WORKDIR "/src/Jx.Gateway"
RUN dotnet build "Jx.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jx.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jx.Gateway.dll"]
