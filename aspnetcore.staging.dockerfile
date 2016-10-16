FROM microsoft/dotnet:latest

MAINTAINER Aivan Monceller

ENV ASPNETCORE_ENVIRONMENT staging
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

WORKDIR /app/src/Zervo

RUN dotnet restore

RUN dotnet build

EXPOSE 5090/tcp

ENTRYPOINT ["dotnet", "watch", "run", "--server.urls", "http://0.0.0.0:5090"]

#ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]


# Build the image:
# docker build -f aspnetcore.development.dockerfile -t [yourDockerHubID]/dotnet:1.0.0 

# Option 1
# Start PostgreSQL and ASP.NET Core (link ASP.NET core to ProgreSQL container with legacy linking)
 
# docker run -d --name my-postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d -p 5000:5000 --link my-postgres:postgres [yourDockerHubID]/dotnet:1.0.0

# Option 2: Create a custom bridge network and add containers into it

# docker network create --driver bridge isolated_network
# docker run -d --net=isolated_network --name postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d --net=isolated_network --name aspnetcoreapp -p 5000:5000 [yourDockerHubID]/dotnet:1.0.0