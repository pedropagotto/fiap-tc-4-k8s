FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../ContactWorker/ContactWorker.csproj", "./ContactWorker/"]
COPY ["../ContactApi/ContactApi.csproj", "./ContactApi/"]
COPY ["../Application/Application.csproj", "./Application/"]
COPY ["../Common/Common.csproj", "./Common/"]
COPY ["../Domain/Domain.csproj", "./Domain/"]
COPY ["../Infra/Infra.csproj", "./Infra/"]
RUN dotnet restore "./ContactWorker/ContactWorker.csproj"
COPY . .
WORKDIR "/src/ContactWorker"
RUN dotnet build "ContactWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContactWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactWorker.dll"]
