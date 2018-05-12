# Hard Reset
"Remove all bin and obj Files"

$source = "Century.Api"
$test = "Century.Tests"
$tools = "Tools"

if (Test-Path $source/bin) { Remove-Item $source/bin -Force -Recurse }
if (Test-Path $source/obj) { Remove-Item $source/obj -Force -Recurse }
if (Test-Path $test/bin) { Remove-Item $test/bin -Force -Recurse }
if (Test-Path $test/obj) { Remove-Item $test/obj -Force -Recurse }
if (Test-Path $tools/bin) { Remove-Item $tools/bin -Force -Recurse }
if (Test-Path $tools/obj) { Remove-Item $tools/obj -Force -Recurse }

"Remove all Coverage files"
if (Test-Path coverage-html) { Remove-Item coverage-html -Force -Recurse }
if (Test-Path coverage.json) { Remove-Item coverage.json }
if (Test-Path coverage.xml) { Remove-Item coverage.xml }
if (Test-Path coverage-hits.txt) { Remove-Item coverage-hits.txt }

dotnet restore
dotnet build

Push-Location ./$tools

dotnet restore

dotnet minicover instrument --workdir ../ --assemblies ./$test/bin/**/**/*.dll --sources ./$source/**/*.cs

# Reset hits count in case minicover was run for this project
dotnet minicover reset

Pop-Location
Push-Location ./$test

dotnet test --no-build $project

Pop-Location
Push-Location ./$tools

# Uninstrument assemblies, it's important if you're going to publish or deploy build outputs
dotnet minicover uninstrument --workdir ../

# Create html reports inside folder coverage-html
dotnet minicover htmlreport --workdir ../ --threshold 90

# Print console report
# This command returns failure if the coverage is lower than the threshold
dotnet minicover report --workdir ../ --threshold 90

dotnet minicover xmlreport --workdir ../ --threshold 90

./report-generator/ReportGenerator.exe -reports:../coverage.xml -targetdir:../reports/report-out

Pop-Location
