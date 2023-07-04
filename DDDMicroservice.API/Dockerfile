FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
# allow weak certificates (certificate signed with SHA1)
# by downgrading OpenSSL security level from 2 to 1
RUN sed -i 's/SECLEVEL=2/SECLEVEL=1/g' /etc/ssl/openssl.cnf

ENV ASPNETCORE_URLS=http://+:80

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DDDMicroservice.API/DDDMicroservice.API.csproj", "DDDMicroservice.API/"]
RUN dotnet restore "DDDMicroservice.API/DDDMicroservice.API.csproj"
RUN dotnet tool restore
COPY . .
WORKDIR "/src/DDDMicroservice.API"
RUN dotnet build "DDDMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDDMicroservice.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDDMicroservice.API.dll"]
