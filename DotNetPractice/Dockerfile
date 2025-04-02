# Use the official .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files and restore any dependencies
COPY . ./
RUN dotnet restore

# Build the project
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as a runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /app/out .

# Set environment variables
ENV ASPNETCORE_URLS=http://localhost:5024
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the port
EXPOSE 5024

# Run the application
ENTRYPOINT ["dotnet", "DotNetPractice.dll"]