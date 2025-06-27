# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 5000 and 5003 (for http/https)
EXPOSE 5000
EXPOSE 5003

ENV ASPNETCORE_URLS=http://+:5000;https://+:5003
ENTRYPOINT ["dotnet", "POSInventory.dll"]
