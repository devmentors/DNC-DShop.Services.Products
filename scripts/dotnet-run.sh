#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/DShop.Services.Products
dotnet run --no-restore