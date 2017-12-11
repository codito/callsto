#!/bin/sh

set -o nounset  # Fail on uninitialized variables.

CALLSTO_ROOT=$(dirname $0)
CONFIGURATION=Debug

function watch()
{
    while [[ true ]]; do
        # entr exits when a directory changes, i.e. when file added, removed etc.
        # the loop keeps it running always with the added/removed files included
        git --git-dir=./.git ls-files -oc --exclude-standard | entr -d dotnet test $CALLSTO_ROOT/test/Callsto.Tests/Callsto.Tests.csproj
    done
}

watch
