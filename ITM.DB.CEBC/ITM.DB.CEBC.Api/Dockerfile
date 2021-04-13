#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ITM.DB.CEBC.Api/ITM.DB.CEBC.Api.csproj", "ITM.DB.CEBC.Api/"]
RUN dotnet restore "ITM.DB.CEBC.Api/ITM.DB.CEBC.Api.csproj"
COPY . .
WORKDIR "/src/ITM.DB.CEBC.Api"
RUN dotnet build "ITM.DB.CEBC.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITM.DB.CEBC.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITM.DB.CEBC.Api.dll"]