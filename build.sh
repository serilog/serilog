#!/bin/bash
dotnet restore
for path in src/*/project.json; do
    dirname="$(dirname "${path}")"
    dotnet build ${dirname} -c Release
done

for path in test/Serilog.Tests/project.json; do
    dirname="$(dirname "${path}")"
    dotnet build ${dirname} -f netcoreapp1.0 -c Release
    dotnet test ${dirname} -f netcoreapp1.0  -c Release
done

for path in test/Serilog.PerformanceTests/project.json; do
    dirname="$(dirname "${path}")"
    dotnet build ${dirname} -f netcoreapp1.0 -c Release
    dotnet test ${dirname} -f netcoreapp1.0  -c Release
done