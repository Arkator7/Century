#!/bin/bash
BASEPROJECT=${PWD}
PROJECTNAME=${PWD##*/}

# Test Coverage
dotnet restore

dotnet build

source='Api'
test='Tests'
tools='Tools'

# push-location
cd $BASEPROJECT/$tools

dotnet restore

dotnet minicover instrument --workdir ../ --assemblies ./$PROJECTNAME.$test/bin/**/**/*.dll --sources ./$PROJECTNAME.$source/**/*.cs

# Reset hits count in case minicover was run for this project
dotnet minicover reset

cd $BASEPROJECT/$PROJECTNAME.$test

dotnet test --no-build $project

cd $BASEPROJECT/$tools

# Uninstrument assemblies, it's important if you're going to publish or deploy build outputs
dotnet minicover uninstrument --workdir ../

# Create html reports inside folder coverage-html
dotnet minicover htmlreport --workdir ../ --threshold 90

# Print console report
# This command returns failure if the coverage is lower than the threshold
dotnet minicover report --workdir ../ --threshold 90

read -p "Press enter to continue"
