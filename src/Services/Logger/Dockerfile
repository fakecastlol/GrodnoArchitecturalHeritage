FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY "Logger.API/Logger.API.csproj" "Logger.API/Logger.API.csproj"

RUN dotnet restore "Logger.API/Logger.API.csproj"

COPY . .

FROM build AS publish
WORKDIR /publish

COPY --from=build /app .
RUN dotnet publish "Logger.API/Logger.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=publish /publish/out .

ENTRYPOINT ["dotnet", "Logger.API.dll"]