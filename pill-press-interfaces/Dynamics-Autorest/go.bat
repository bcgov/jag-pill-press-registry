@echo off

echo Updating meta data
set NODE_OPTIONS=--max-old-space-size=16384
dotnet run -p ..\OData.OpenAPI\odata2openapi\odata2openapi.csproj bcgov

echo Updating client

rem autorest --debug --verbose Readme.md

autorest --verbose --input-file=dynamics-swagger.json --output-folder=.  --csharp --use-datetimeoffset --generate-empty-classes --override-client-name=DynamicsClient  --namespace=Gov.Jag.PillPressRegistry.Interfaces --preview  --add-credentials --debug

