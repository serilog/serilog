#!/bin/bash
dotnet restore

for path in test/*.PerformanceTests/*.csproj; do
    dotnet test -f netcoreapp1.1 -c Release ${path}
done
