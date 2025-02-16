FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../ContactReadApi/ContactReadApi.csproj", "./ContactReadApi/"]
COPY ["../Application/Application.csproj", "./Application/"]
COPY ["../Common/Common.csproj", "./Common/"]
COPY ["../Domain/Domain.csproj", "./Domain/"]
COPY ["../Infra/Infra.csproj", "./Infra/"]
RUN dotnet restore "./ContactReadApi/ContactReadApi.csproj"
COPY . /src
WORKDIR "/src/ContactReadApi"
RUN dotnet build "ContactReadApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContactReadApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactReadApi.dll"]
