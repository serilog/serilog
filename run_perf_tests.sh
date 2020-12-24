#!/bin/bash
dotnet restore

for path in test/*.PerformanceTests/*.csproj; do
    dotnet run ${path} -c Release --framework net5.0 -- --filter *
done
