#!/bin/bash

set -e

dotnet --info
dotnet restore

echo "🤖 Attempting to build..."
for path in src/**/*.csproj; do
    dotnet build -c Release ${path}
done

echo "🤖 Running tests..."
for path in test/*.Tests/*.csproj; do
    dotnet test -f netcoreapp2.1 -c Release ${path}
    dotnet test -f netcoreapp3.1 -c Release ${path}
    dotnet test -f net5.0 -c Release ${path}
done

for path in test/*.PerformanceTests/*.PerformanceTests.csproj; do
    dotnet build -c Release ${path}
done
