using Google.Cloud.DiscoveryEngine.V1Beta;

namespace SearchConversation;

/// <summary>
/// This class provides a simple wrapper for the Discovery Engine API's Search.
/// </summary>
public class DiscoveryEngineSearch(string projectId, string dataStoreId)
{
    /* The raw REST call looks like this:

            curl -X POST -H "Authorization: Bearer $(gcloud auth print-access-token)" \
        -H "Content-Type: application/json" \
        "https://discoveryengine.googleapis.com/v1alpha/projects/xxxxxxx/locations/global/collections/default_collection/dataStores/yyyyy/servingConfigs/default_search:search" \
        -d '{"query":"Give me your best guess","pageSize":10,"queryExpansionSpec":{"condition":"AUTO"},"spellCorrectionSpec":{"mode":"AUTO"},"contentSearchSpec":{"summarySpec":{"summaryResultCount":5,"ignoreAdversarialQuery":true,"includeCitations":true},"snippetSpec":{"returnSnippet":true},"extractiveContentSpec":{"maxExtractiveAnswerCount":1}}}'
    */

    public async Task<SearchResponse> SearchAsync(string prompt)
    {
        var request = CreateRequest(prompt);

        var client = await SearchServiceClient.CreateAsync();

        var response = client.Search(request).AsRawResponses()
            .FirstOrDefault();

        return response;
    }

    private SearchRequest CreateRequest(string prompt)
    {
        var request = new SearchRequest()
        {
            ServingConfig = $"projects/{projectId}/locations/global/collections/default_collection/dataStores/{dataStoreId}/servingConfigs/default_serving_config",
            PageSize = 10,
            Query = prompt,
            QueryExpansionSpec = new SearchRequest.Types.QueryExpansionSpec()
            {
                Condition = SearchRequest.Types.QueryExpansionSpec.Types.Condition.Auto,
            },
            SpellCorrectionSpec = new SearchRequest.Types.SpellCorrectionSpec()
            {
                Mode = SearchRequest.Types.SpellCorrectionSpec.Types.Mode.Auto,
            },
            ContentSearchSpec = new SearchRequest.Types.ContentSearchSpec()
            {
                SummarySpec = new SearchRequest.Types.ContentSearchSpec.Types.SummarySpec()
                {
                    IncludeCitations = true,
                    SummaryResultCount = 5,
                    IgnoreAdversarialQuery = true,
                    
                },
                SnippetSpec = new SearchRequest.Types.ContentSearchSpec.Types.SnippetSpec() 
                {
                    ReturnSnippet = true,
                    
                },
                ExtractiveContentSpec = new SearchRequest.Types.ContentSearchSpec.Types.ExtractiveContentSpec()
                {
                    MaxExtractiveAnswerCount = 1,
                }
            },

        };
        return request;
    }
}