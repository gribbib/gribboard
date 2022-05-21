FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore GribBoard.csproj



# Required libraries in Unix
RUN apt-get update -q && apt-get install -q -y \
        curl apt-transport-https apt-utils dialog \
        make g++ build-essential

# Node Bash Script for Debian
# https://github.com/nodesource/distributions#deb
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -

# Propagate Node for Docker
RUN apt-get update \
    && apt-get install -y nodejs


# Copy everything else and build
# COPY / ./
RUN dotnet publish -c Release -o out

#npm install in gribbib-client
# FROM node:16.13.0 as buildvue
# WORKDIR /app
# COPY --from=build-env /app/out .
WORKDIR /app/out/gribboard-client
RUN npm install

# Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/out
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
# COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "GribBoard.dll"]
