#!/bin/bash
dotnet --info
dotnet restore

for path in src/**/*.csproj; do
    dotnet build -f netstandard1.0 -c Release ${path}
    dotnet build -f netstandard1.3 -c Release ${path}
done

for path in test/*.Tests/*.csproj; do
    dotnet test -f netcoreapp1.0  -c Release ${path}
done

for path in test/*.PerformanceTests/*.PerformanceTests.csproj; do
    dotnet build -f netcoreapp1.1 -c Release ${path}
    # dotnet test -f netcoreapp1.1 -c Release ${path}
done
