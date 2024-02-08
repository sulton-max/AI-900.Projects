using System.Net.Http.Headers;
using BikeUsers.Api.Configurations;
using BikeUsers.Application.BikeUsages.Brokers;
using BikeUsers.Application.BikeUsages.Models;


var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();


var bikeUsageBroker = app.Services.GetRequiredService<IBikeUsagePredictionApiBroker>();
var test = await bikeUsageBroker.GetPredictedUsersAsync(
    new BikeUsageRequest
    {
        Date = new DateTimeOffset(new DateTime(2024, 1, 1)),
        Hour = 3,
        WeatherSituation = WeatherSituation.Mist,
        Temperature = 32.0,
        FeelTemperature = 32.0,
        Humidity = 3.0,
        WindSpeed = 3.0,
        Casual = 4
    }
);

await app.RunAsync();


var handler = new HttpClientHandler
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
};

using var client = new HttpClient(handler);

// Request data goes here
// The example below assumes JSON formatting which may be updated
// depending on the format your endpoint expects.
// More information can be found here:
// https://docs.microsoft.com/azure/machine-learning/how-to-deploy-advanced-entry-script

var requestBody = @"{
                  ""Inputs"": {
                    ""data"": [
                      {
                        ""Column2"": ""example_value"",
                        ""dteday"": ""2024-01-01T00:00:00.000Z"",
                        ""hr"": 2,
                        ""weathersit"": 0,
                        ""temp"": 32.0,
                        ""atemp"": 32.0,
                        ""hum"": 3.0,
                        ""windspeed"": 3.0,
                        ""casual"": 4
                      }
                    ]
                  },
                  ""GlobalParameters"": 0.0
                }";

// Replace this with the primary/secondary key or AMLToken for the endpoint
const string apiKey = "EsGvctRIRvMsZ7xcZz2zzjXKnQSNMsJp";
if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("A key should be provided to invoke the endpoint");
}

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
client.BaseAddress = new Uri("http://6b0bb16f-1cd1-45f8-b2fb-e1d660f5ed75.eastus2.azurecontainer.io/score");

var content = new StringContent(requestBody);
content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

// WARNING: The 'await' statement below can result in a deadlock
// if you are calling this code from the UI thread of an ASP.Net application.
// One way to address this would be to call ConfigureAwait(false)
// so that the execution does not attempt to resume on the original context.
// For instance, replace code such as:
//      result = await DoSomeTask()
// with the following:
//      result = await DoSomeTask().ConfigureAwait(false)
HttpResponseMessage response = await client.PostAsync("", content);

if (response.IsSuccessStatusCode)
{
    string result = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Result: {0}", result);
}
else
{
    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

    // Print the headers - they include the requert ID and the timestamp,
    // which are useful for debugging the failure
    Console.WriteLine(response.Headers.ToString());

    string responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseContent);
}