#!/bin/bash

set -e

dotnet --info
dotnet restore

echo "🤖 Attempting to build..."

dotnet build -configuration Release

echo "🤖 Running tests..."
dotnet test --configuration Release --no-build --no-restore
