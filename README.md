# Discovery Engine Sample
The Google .NET SDK includes classes for interacting with the Vertex Search & Converation.  The API is in the DiscoveryEngine library and is discoverable from within the GCP Console through the **Integration** tab:

![gcp console](gcp-console-vertex.png)

```bash
curl -X POST -H "Authorization: Bearer $(gcloud auth print-access-token)" \
    -H "Content-Type: application/json" \
    "https://discoveryengine.googleapis.com/v1alpha/projects/xxxxxxx/locations/global/collections/default_collection/dataStores/yyyyy/servingConfigs/default_search:search" \
    -d '{"query":"Give me your best guess","pageSize":10,"queryExpansionSpec":{"condition":"AUTO"},"spellCorrectionSpec":{"mode":"AUTO"},"contentSearchSpec":{"summarySpec":{"summaryResultCount":5,"ignoreAdversarialQuery":true,"includeCitations":true},"snippetSpec":{"returnSnippet":true},"extractiveContentSpec":{"maxExtractiveAnswerCount":1}}}'
```

## Authenticating with Google Cloud APIs

We are going to use the GCP [Application Default Credentials](https://cloud.google.com/docs/authentication/application-default-credentials) which provides an easy abstraction to authenticate with GCP APIs.  When running locally it will look for an environment variable named `GOOGLE_APPLICATION_CREDENTIALS` which contains the path to a JSON file with your GCP credentials.  When you deploy to GCP (i.e. Cloud Run, GKE, GCE, etc...) the SDK will automatically authenticate using the metadata server, so you don't have to pass in any credentials.

Use these [instructions](https://cloud.google.com/docs/authentication/application-default-credentials#personal) to generate your credentials file.  For example on linux follow these steps:

```bash
gcloud auth application-default login 

export GOOGLE_APPLICATION_CREDENTIALS=~/.config/gcloud/application_default_credentials.json
```

## Running the Sample

Set project and datastore values in `appsettings.Development.json`, for example
```json
{
  "ProjectId": "MyGcpProject1234",
  "DataStoreId": "MyDataStore-1234"
}
```

Run the app in development mode to read those settings with:
```bash
DOTNET_ENVIRONMENT=Development dotnet run
```

## Notes
### Prerelease Google SDK Packages
This application is using a pre-release version of the Google.Cloud.DiscoveryEngine APIs, specfivially `1.0.0-beta09`.  To install a pre-release version of the APIs, run:

```bash
dotnet add package --version 1.0.0-beta09 Google.Cloud.DiscoveryEngine.V1Beta  
```

Or to grab the latest prerelease just add the `--prerelease` flag and notice that the namespace is `V1Beta` instead of `V1`.


```bash
dotnet add package --prerelease Google.Cloud.DiscoveryEngine.V1Beta  
```
