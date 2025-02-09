FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ContactWorker/ContactWorker.csproj ./ContactWorker/

RUN dotnet restore ContactWorker/ContactWorker.csproj
COPY . .

WORKDIR /src/ContactWorker/

RUN dotnet build ContactWorker.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ContactWorker.dll"]
