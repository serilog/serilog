#!/bin/bash
dotnet restore
for path in src/**/*.csproj; do
    dotnet build -c Release ${path}
done

for path in test/**/*.csproj; do
    dotnet build -f netcoreapp1.0 -c Release ${path}
    dotnet test -f netcoreapp1.0  -c Release ${path}
done

for path in test/Serilog.PerformanceTests/Serilog.PerformanceTests.csproj; do
    dotnet build -f netcoreapp1.0 -c Release ${path}
done
