/// <summary>
/// This application hosts a REST endpoint that queries an LLM using the Google Vertex Search and Conversation API.
/// </summary>
using SearchConversation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AllowAllCors();
var app = builder.Build();

app.UseCors();

// Map /ask endpoint to Vertex Search
app.MapGet("/ask", async (string prompt) => 
{
    try
    {
        var result = await Search(prompt);
        if (result == null)
        {
            return Results.NotFound("No response from Vertex Search");
        }
        else
        {
            return Results.Ok(result);
        }
    }
    catch (Exception error)
    {
        return Results.BadRequest(error.Message);
    }
});

app.Run();

/// <summary>
/// Runs a query against Vertex Search and Conversation.
/// </summary>
async Task<string> Search(string prompt)
{
    var config = builder.Configuration;

    string projectId = config["projectId"];
    string dataStoreId = config["dataStoreId"];

    if (string.IsNullOrEmpty(projectId))
        throw new Exception("Missing configuration variable: projectId");

    var client = new DiscoveryEngineSearch(projectId, dataStoreId);

    var response = await client.SearchAsync(prompt);

    return response?.Summary.SummaryText ?? null;
}
