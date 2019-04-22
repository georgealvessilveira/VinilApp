FROM microsoft/dotnet:2.1-sdk AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["VinilApp.RestApi/VinilApp.RestApi.csproj", "VinilApp.RestApi/"]
RUN dotnet restore "VinilApp.RestApi/VinilApp.RestApi.csproj"
COPY . .
WORKDIR "/src/VinilApp.RestApi"
RUN dotnet build "VinilApp.RestApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VinilApp.RestApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VinilApp.RestApi.dll"]]