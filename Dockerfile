FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
WORKDIR /src

COPY ["baevii-back-end/baevii-back-end.csproj", "."]
RUN dotnet restore "baevii-back-end.csproj"
COPY ["baevii-back-end/*", "."]
RUN dotnet build "baevii-back-end.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "baevii-back-end.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt update && apt install tzdata
RUN groupadd -r dotnet && useradd -r -g dotnet dotnet
USER dotnet
ENV ASPNETCORE_URLS=http://*:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "baevii-back-end.dll"]
