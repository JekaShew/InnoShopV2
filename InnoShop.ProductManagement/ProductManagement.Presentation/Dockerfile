#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["ProductManagement.Presentation/ProductManagement.Presentation.csproj", "ProductManagement.Presentation/"]
COPY ["ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj", "ProductManagement.Infrastructure/"]
COPY ["ProductManagement.Application/ProductManagement.Application.csproj", "ProductManagement.Application/"]
COPY ["ProductManagement.Domain/ProductManagement.Domain.csproj", "ProductManagement.Domain/"]
COPY ["../InnoShop.CommonLibrary/InnoShop.CommonLibrary/InnoShop.CommonLibrary.csproj", "InnoShop.CommonLibrary/InnoShop.CommonLibrary/"]
RUN dotnet restore "./ProductManagement.Presentation/ProductManagement.Presentation.csproj"

COPY . .

WORKDIR "/src/ProductManagement.Presentation"
RUN dotnet build "./ProductManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ProductManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.Presentation.dll"]

#
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 5001
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ProductManagement.Presentation/ProductManagement.Presentation.csproj", "ProductManagement.Presentation/"]
#COPY ["ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj", "ProductManagement.Infrastructure/"]
#COPY ["ProductManagement.Application/ProductManagement.Application.csproj", "ProductManagement.Application/"]
#COPY ["ProductManagement.Domain/ProductManagement.Domain.csproj", "ProductManagement.Domain/"]
#COPY ["ProductManagement.CommonLibarary/ProductManagement.CommonLibarary.csproj", "ProductManagement.CommonLibarary/"]
#RUN dotnet restore "./ProductManagement.Presentation/ProductManagement.Presentation.csproj"
#COPY . .
#WORKDIR "/src/ProductManagement.Presentation"
#RUN dotnet build "./ProductManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ProductManagement.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ProductManagement.Presentation.dll"]