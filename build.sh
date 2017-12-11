#!/bin/sh

set -o nounset  # Fail on uninitialized variables.
set -e          # Fail on non-zero exit code.

CONFIGURATION=Debug

function publish()
{
    # Publish the binary
    dotnet publish --output $(pwd)/artifacts/$CONFIGURATION
}

publish
