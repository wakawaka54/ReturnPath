FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 1410/tcp

ENTRYPOINT ["dotnet", "run", "--port", "1410"]