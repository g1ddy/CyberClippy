#!/bin/bash
set -e

# Project name
PROJECT_NAME=Clippy

# .NET version
DOTNET_VERSION=10.0

# Update package sources
sudo apt-get update
sudo apt-get install -y apt-transport-https

# Install .NET SDK from Ubuntu repositories
# Note: apt-get update was already run above
# Check if dotnet is already installed to avoid unnecessary reinstall/sudo
if ! command -v dotnet &> /dev/null; then
    sudo apt-get install -y dotnet-sdk-$DOTNET_VERSION
else
    echo "dotnet is already installed."
fi

# Test: display version
dotnet --version

# --- IMPORTANT: Restore ALL packages while Internet is available! ---
# Use EnableWindowsTargeting to allow restoring Windows-specific projects on Linux
dotnet restore $PROJECT_NAME.sln /p:EnableWindowsTargeting=true

# (Optional: Build and test immediately while Internet is available)
# Commented out to prevent interference with snapshot creation
# dotnet build $PROJECT_NAME.sln --no-restore
# dotnet test $PROJECT_NAME.sln --no-build --no-restore

echo "Setup and pre-restore completed. Container is ready for offline use."
