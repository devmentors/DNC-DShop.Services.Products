FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /app

ARG sln=DShop.Services.Products.sln
ARG service=src/DShop.Services.Products
ARG tests=tests/DShop.Services.Products.Tests
ARG configuration=Release
ARG feed='--source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/chronicle/api/v3/index.json"'

COPY ${sln} ./
COPY ./${service} ./${service}/
COPY ./${tests} ./${tests}/

RUN dotnet restore /property:Configuration=${configuration} ${feed}

COPY . ./
RUN dotnet publish ${service} -c ${configuration} -o out ${feed}

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app

ARG service=src/DShop.Services.Products

COPY --from=builder /app/${service}/out/ .

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker

EXPOSE 5000

ENTRYPOINT dotnet DShop.Services.Products.dll