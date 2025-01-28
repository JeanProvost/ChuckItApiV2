FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src
COPY ["ChuckItApiV2/ChuckItApiV2.csproj", "ChuckItApiV2/"]
RUN dotnet restore "ChuckItApiV2/ChuckItApiV2.csproj"
COPY . .
WORKDIR "/src/ChuckItApiV2"
RUN dotnet build "ChuckItApiV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChuckItApiV2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChuckItApiV2.dll"]