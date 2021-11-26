FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TPost.Host.WebHost/TPost.Host.WebHost.csproj", "ATPost.Host.WebHost/"]
RUN dotnet restore "TPost.Host.WebHost/TPost.Host.WebHost.csproj"
COPY . .
WORKDIR "/src/TPost.Host.WebHost"
RUN dotnet build "TPost.Host.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPost.Host.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPost.Host.WebHost.dll"]
