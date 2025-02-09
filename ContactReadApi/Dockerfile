FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8084
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./ContactReadApi/ContactReadApi.csproj", "./ContactReadApi/"]
RUN dotnet restore "./ContactReadApi/ContactReadApi.csproj"
COPY . .
WORKDIR "/src/ContactReadApi"
RUN dotnet build "ContactReadApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContactReadApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8084
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "ContactReadApi.dll"]
