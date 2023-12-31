FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /PlatformService

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /PlatformService
COPY --from=build-env /PlatformService/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]