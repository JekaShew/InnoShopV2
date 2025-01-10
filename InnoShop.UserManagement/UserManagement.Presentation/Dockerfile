#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["UserManagement.Presentation/UserManagement.Presentation.csproj", "UserManagement.Presentation/"]
COPY ["UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "UserManagement.Infrastructure/"]
COPY ["UserManagement.Domain/UserManagement.Domain.csproj", "UserManagement.Domain/"]
COPY ["UserManagement.Application/UserManagement.Application.csproj", "UserManagement.Application/"]
RUN dotnet restore "UserManagement.Presentation/UserManagement.Presentation.csproj"

COPY . .

WORKDIR "/src/UserManagement.Presentation"
RUN dotnet build "UserManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "UserManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Presentation.dll"]