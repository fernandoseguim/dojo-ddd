FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/DojoDDD.Api/DojoDDD.Api.csproj", "src/DojoDDD.Api/"]
COPY ["src/DojoDDD.Application/DojoDDD.Application.csproj", "src/DojoDDD.Application/"]
COPY ["src/DojoDDD.Domain/DojoDDD.Domain.csproj", "src/DojoDDD.Domain/"]
COPY ["src/DojoDDD.Infra/DojoDDD.Infra.csproj", "src/DojoDDD.Infra/"]
RUN dotnet restore "src/DojoDDD.Api/DojoDDD.Api.csproj"
COPY . .
WORKDIR "/src/src/DojoDDD.Api"
RUN dotnet build "DojoDDD.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app -r linux-musl-x64

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS final
WORKDIR /app
ENV TZ=America/Sao_Paulo
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DojoDDD.Api.dll"]