#DemoTool
Simple test application that connects to the environment specified domain and deletes ALL incidents, business profiles, business contacts, and contacts in preparation for demos.

The application may be run locally with:
dotnet build
dotnet run clean

NOTE: local execution depends on the configuration of the following secrets as environment variables:
export DYNAMICS_ODATA_URI
export DYNAMICS_NATIVE_ODATA_URI
export DYNAMICS_AAD_TENANT_ID
export DYNAMICS_SERVER_APP_ID_URI
export DYNAMICS_CLIENT_KEY
export DYNAMICS_CLIENT_ID
These secrets should NEVER be committed to source control.