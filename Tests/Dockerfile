# Use the SDK image as the base image for building and testing
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Tests/Tests.csproj", "Tests/"]
RUN dotnet restore "Tests/Tests.csproj"

# Copy the entire solution and build the tests
COPY . .
WORKDIR "/src/Tests"
RUN dotnet build "Tests.csproj" -c Release -o /app/build

# Run the tests
CMD ["dotnet", "test", "--logger:trx"]