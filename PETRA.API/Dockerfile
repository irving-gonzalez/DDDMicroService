FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5194

ENV ASPNETCORE_URLS=http://+:5194

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["DDDMicroService/PETRA.API/PETRA.API.csproj", "DDDMicroService/PETRA.API/"]
RUN dotnet restore "DDDMicroService/PETRA.API/PETRA.API.csproj"
RUN dotnet tool restore
COPY . .
WORKDIR "/src/DDDMicroService/PETRA.API"
RUN dotnet build "PETRA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PETRA.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PETRA.API.dll"]
