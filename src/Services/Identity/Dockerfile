FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY "Identity.API/Identity.API.csproj" "Identity.API/Identity.API.csproj"
COPY "Domain/Identity.Domain.Core/Identity.Domain.Core.csproj" "Domain/Identity.Domain.Core/Identity.Domain.Core.csproj"
COPY "Domain/Identity.Domain.Interfaces/Identity.Domain.Interfaces.csproj" "Domain/Identity.Domain.Interfaces/Identity.Domain.Interfaces.csproj"
COPY "Infrastructure/Identity.Infrastructure.Business/Identity.Infrastructure.Business.csproj" "Infrastructure/Identity.Infrastructure.Business/Identity.Infrastructure.Business.csproj"
COPY "Infrastructure/Identity.Infrastructure.Data/Identity.Infrastructure.Data.csproj" "Infrastructure/Identity.Infrastructure.Data/Identity.Infrastructure.Data.csproj"
COPY "Services/Identity.Services.Interfaces/Identity.Services.Interfaces.csproj" "Services/Identity.Services.Interfaces/Identity.Services.Interfaces.csproj"


RUN dotnet restore "Identity.API/Identity.API.csproj"

COPY . .

FROM build AS publish
WORKDIR /publish

COPY --from=build /app .
RUN dotnet publish "Identity.API/Identity.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=publish /publish/out .

ENTRYPOINT ["dotnet", "Identity.API.dll"]