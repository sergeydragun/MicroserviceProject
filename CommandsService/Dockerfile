FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /CommandsService

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /CommandsService
COPY --from=build-env /CommandsService/out .
ENTRYPOINT ["dotnet", "CommandsService.dll"]