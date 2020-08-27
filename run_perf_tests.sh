#!/bin/bash
dotnet restore

for path in test/*.PerformanceTests/*.csproj; do
    dotnet run ${path} -c Release --framework netcoreapp3.1 -- --filter *
done
