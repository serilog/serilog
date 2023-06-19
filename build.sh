#!/bin/bash

set -e

echo "🤖 Attempting to build..."

dotnet build --configuration Release

echo "🤖 Running tests..."
dotnet test test/Serilog.Tests --configuration Release --no-build --no-restore
