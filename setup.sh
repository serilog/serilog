#!/bin/bash

RequiredDotnetVersion=$(jq -r '.sdk.version' global.json)

curl https://dot.net/v1/dotnet-install.sh -sSf | sh -s -- --install-dir $HOME/.dotnetcli --no-path --version $RequiredDotnetVersion
