# Use the .NET SDK for the final image (needed for EF tools)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /src
EXPOSE 8080

# Copy project file and restore
COPY ["RainTrackingApi.csproj", "./"]
RUN dotnet restore "RainTrackingApi.csproj"

# Copy source code
COPY . .

# Build the application
RUN dotnet build "RainTrackingApi.csproj" -c Release -o /app/build

# Install EF Core tools for database migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Set working directory for the app
WORKDIR /app/build

ENTRYPOINT ["dotnet", "RainTrackingApi.dll"]