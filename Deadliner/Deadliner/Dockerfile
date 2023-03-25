FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Deadliner/Deadliner.csproj", "Deadliner/"]
RUN dotnet restore "Deadliner/Deadliner.csproj"
COPY . .
WORKDIR "/src/Deadliner"
RUN dotnet build "Deadliner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deadliner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deadliner.dll"]
