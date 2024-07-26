# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["EcommerceApp/EcommerceApp.csproj", "EcommerceApp/"]
COPY ["Ecommerce.DB/Ecommerce.DB.csproj", "Ecommerce.DB/"]
COPY ["Ecommerce.Domain/Ecommerce.Domain.csproj", "Ecommerce.Domain/"]
COPY ["Ecommerce.Domain.Shared/Ecommerce.Domain.Shared.csproj", "Ecommerce.Domain.Shared/"]

RUN dotnet restore "EcommerceApp/EcommerceApp.csproj"

# Copy the rest of the application code and build it
COPY . .
RUN dotnet build "EcommerceApp/EcommerceApp.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "EcommerceApp/EcommerceApp.csproj" -c Release -o /app/publish

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceApp.dll"]
