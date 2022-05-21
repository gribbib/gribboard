# build .NET app:
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as buildnet

WORKDIR /src

COPY GribBoard.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build -c Release

# RUN dotnet test ...

RUN dotnet publish -c Release -o /dist


# build Vue app:
FROM node:16.13.0 as buildvue

WORKDIR /src

COPY gribboard-client/package.json .
RUN npm install

# webpack build
COPY gribboard-client .
RUN npm run build


# Copy results from both places into production container:
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# copy .net content
COPY --from=buildnet /dist .
# copy vue content into .net's static files folder:
COPY --from=buildvue ./src/wwwroot/dist /app/wwwroot
RUN ls -ls /app/wwwroot

CMD ["dotnet", "GribBoard.dll"]