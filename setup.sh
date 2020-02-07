#!/bin/bash

sudo apt-get update && sudo apt-get install -y --no-install-recommends jq

RequiredDotnetVersion=$(jq -r '.sdk.version' global.json)

curl https://dot.net/v1/dotnet-install.sh -sSfL | bash -s -- --install-dir $HOME/.dotnetcli --no-path --version $RequiredDotnetVersion
