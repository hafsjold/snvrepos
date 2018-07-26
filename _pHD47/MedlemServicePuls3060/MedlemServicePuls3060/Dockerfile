FROM microsoft/dotnet:2.1-runtime-nanoserver-1709 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY MedlemServicePuls3060/MedlemServicePuls3060.csproj MedlemServicePuls3060/
COPY Pbs3060/Pbs3060.csproj Pbs3060/
RUN dotnet restore MedlemServicePuls3060/MedlemServicePuls3060.csproj
COPY . .
WORKDIR /src/MedlemServicePuls3060
RUN dotnet build MedlemServicePuls3060.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MedlemServicePuls3060.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MedlemServicePuls3060.dll"]
