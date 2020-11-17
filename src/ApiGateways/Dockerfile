FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY "Web.Platform.HttpAggregator/Web.Platform.HttpAggregator.csproj" "Web.Platform.HttpAggregator/Web.Platform.HttpAggregator.csproj"
RUN dotnet restore "Web.Platform.HttpAggregator/Web.Platform.HttpAggregator.csproj"

COPY . .

FROM build AS publish
WORKDIR /publish

COPY --from=build /app .
RUN dotnet publish "Web.Platform.HttpAggregator/Web.Platform.HttpAggregator.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=publish publish/out .

ENTRYPOINT ["dotnet", "Web.Platform.HttpAggregator.dll"]