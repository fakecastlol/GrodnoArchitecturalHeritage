FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY "Heritage.API/Heritage.API.csproj" "Heritage.API/Heritage.API.csproj"
COPY "Domain/Heritage.Domain.Core/Heritage.Domain.Core.csproj" "Domain/Heritage.Domain.Core/Heritage.Domain.Core.csproj"
COPY "Domain/Heritage.Domain.Interfaces/Heritage.Domain.Interfaces.csproj" "Domain/Heritage.Domain.Interfaces/Heritage.Domain.Interfaces.csproj"
COPY "Infrastructure/Heritage.Infrastructure.Business/Heritage.Infrastructure.Business.csproj" "Infrastructure/Heritage.Infrastructure.Business/Heritage.Infrastructure.Business.csproj"
COPY "Infrastructure/Heritage.Infrastructure.Data/Heritage.Infrastructure.Data.csproj" "Infrastructure/Heritage.Infrastructure.Data/Heritage.Infrastructure.Data.csproj"
COPY "Services/Heritage.Services.Interfaces/Heritage.Services.Interfaces.csproj" "Services/Heritage.Services.Interfaces/Heritage.Services.Interfaces.csproj"


RUN dotnet restore "Heritage.API/Heritage.API.csproj"

COPY . .

FROM build AS publish
WORKDIR /publish

COPY --from=build /app .
RUN dotnet publish "Heritage.API/Heritage.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=publish /publish/out .

ENTRYPOINT ["dotnet", "Heritage.API.dll"]