FROM fimple.azurecr.io/fi-docker/dotnet/fi-runtime-base:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["Src/Fi.Ticket.Api/Fi.Ticket.Api.csproj", "Src/Fi.Ticket.Api/"]
COPY ["Src/Fi.Ticket.Schema/Fi.Ticket.Schema.csproj", "Src/Fi.Ticket.Schema/"]

RUN dotnet restore "Src/Fi.Ticket.Api/Fi.Ticket.Api.csproj"  

COPY ["Src/", "Src/"]

RUN dotnet publish "Src/Fi.Ticket.Api/Fi.Ticket.Api.csproj" -c Release \
  -o /app/publish

FROM base AS final
COPY --from=build --chown=app:fimple /app/publish .

ENTRYPOINT ["dotnet", "Fi.Ticket.Api.dll"]