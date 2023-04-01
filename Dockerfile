# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
EXPOSE 80
EXPOSE 443

# copy csproj and restore as distinct layers
COPY ["DroneAPI/DroneAPI.csproj", "DroneAPI/"]
COPY ["DroneAPI.BusinessLogic/DroneAPI.Services.csproj", "DroneAPI.BusinessLogic/"]
COPY ["DroneAPI.Core/DroneAPI.Core.csproj", "DroneAPI.Core/"]
COPY ["DroneAPI.DataAccess/DroneAPI.Data.csproj", "DroneAPI.DataAccess/"]
COPY ["DroneAPI.Models/DroneAPI.Models.csproj", "DroneAPI.Models/"]
COPY ["DroneAPI.Repositories/DroneAPI.Repositories.csproj", "DroneAPI.Repositories/"]
RUN dotnet restore "DroneAPI/DroneAPI.csproj"

# copy everything else and build app
COPY . .
WORKDIR "/source/DroneAPI"
RUN dotnet build "DroneAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DroneAPI.csproj" -c Release -o /app
#RUN dotnet publish -c Release -o /app --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_ENVIRONMENT: "Production"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DroneAPI.dll"]
