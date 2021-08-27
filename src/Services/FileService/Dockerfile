FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY "FileService.API/FileService.API.csproj" "FileService.API/FileService.API.csproj"
COPY "Domain/FileService.Domain.Core/FileService.Domain.Core.csproj" "Domain/FileService.Domain.Core/FileService.Domain.Core.csproj"
COPY "Domain/FileService.Domain.Interfaces/FileService.Domain.Interfaces.csproj" "Domain/FileService.Domain.Interfaces/FileService.Domain.Interfaces.csproj"
COPY "Infrastructure/FileService.Infrastructure.Business/FileService.Infrastructure.Business.csproj" "Infrastructure/FileService.Infrastructure.Business/FileService.Infrastructure.Business.csproj"
COPY "Infrastructure/FileService.Infrastructure.Data/FileService.Infrastructure.Data.csproj" "Infrastructure/FileService.Infrastructure.Data/FileService.Infrastructure.Data.csproj"
COPY "Services/FileService.Services.Interfaces/FileService.Services.Interfaces.csproj" "Services/FileService.Services.Interfaces/FileService.Services.Interfaces.csproj"


RUN dotnet restore "FileService.API/FileService.API.csproj"

COPY . .

FROM build AS publish
WORKDIR /publish

COPY --from=build /app .
RUN dotnet publish "FileService.API/FileService.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app

COPY --from=publish /publish/out .

ENTRYPOINT ["dotnet", "FileService.API.dll"]