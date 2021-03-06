FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ToDo/ToDo.csproj", "ToDo/"]
COPY ["MssqlRepository/MssqlRepository.csproj", "MssqlRepository/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Entity/Entity.csproj", "Entity/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["ApiModels/ApiModels.csproj", "ApiModels/"]
COPY ["Logger/Logger.csproj", "Logger/"]
RUN dotnet restore "ToDo/ToDo.csproj"
COPY . .
WORKDIR "/src/ToDo"
RUN dotnet build "ToDo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.dll"]