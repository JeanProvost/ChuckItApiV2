# Stage 1: Base Image for Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: Build and Restore Dependencies
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project file(s) and restore dependencies
COPY ["ChuckItApiV2/ChuckItApiV2.csproj", "ChuckItApiV2/"]
RUN dotnet restore "ChuckItApiV2/ChuckItApiV2.csproj"

# Copy the rest of the source code and build
COPY . .
WORKDIR "/src/ChuckItApiV2"
RUN dotnet build "ChuckItApiV2.csproj" -c Release -o /app/build

# Stage 3: Publish the Application
FROM build AS publish
RUN dotnet publish "ChuckItApiV2.csproj" -c Release -o /app/publish

# Stage 4: Final Image for Runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the default environment
ENV ASPNETCORE_ENVIRONMENT=Development

# Start the application
ENTRYPOINT ["dotnet", "ChuckItApiV2.dll"]
