#!/bin/bash
dotnet restore

for path in test/*.PerformanceTests/*.csproj; do
    dotnet test -f netcoreapp2.0 -c Release ${path}
done
