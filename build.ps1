param()

dotnet restore
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

dotnet build -c Release
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

dotnet test -c Release
