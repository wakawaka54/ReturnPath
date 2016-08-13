FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 1410/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:1410"]
