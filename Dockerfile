# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln .
COPY BookMyMovie.Api/*.csproj ./BookMyMovie.Api/
COPY BookMyMovie.Application/*.csproj ./BookMyMovie.Application/
COPY BookMyMovie.Contracts/*.csproj ./BookMyMovie.Contracts/
COPY BookMyMovie.Domain/*.csproj ./BookMyMovie.Domain/
COPY BookMyMovie.Infrastructure/*.csproj ./BookMyMovie.Infrastructure/

RUN dotnet restore

# Copy everything and build
COPY . .
RUN dotnet publish BookMyMovie.Api/BookMyMovie.Api.csproj -c Release -o /out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose the API port
EXPOSE 5000

# Start the API
ENTRYPOINT ["dotnet", "BookMyMovie.Api.dll"]
