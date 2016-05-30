#!/bin/bash

dotnet restore

cd src/Serilog/

dotnet build -f netstandard1.0 -c Release
dotnet build -f netstandard1.3 -c Release

cd ../..
cd test/Serilog.Tests/ 

dotnet build -f netcoreapp1.0 -c Release
dotnet test -f netcoreapp1.0  -c Release