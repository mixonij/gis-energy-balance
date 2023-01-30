FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ispu.Utils.EnergyBalances.GeoCoding/Ispu.Utils.EnergyBalances.GeoCoding.csproj", "Ispu.Utils.EnergyBalances.GeoCoding/"]
RUN dotnet restore "Ispu.Utils.EnergyBalances.GeoCoding/Ispu.Utils.EnergyBalances.GeoCoding.csproj"
COPY . .
WORKDIR "/src/Ispu.Utils.EnergyBalances.GeoCoding"
RUN dotnet build "Ispu.Utils.EnergyBalances.GeoCoding.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ispu.Utils.EnergyBalances.GeoCoding.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ispu.Utils.EnergyBalances.GeoCoding.dll"]
