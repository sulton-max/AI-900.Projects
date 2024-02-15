using FaqApp.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();


// app.Run();

// var endpoint = "https://max-lang-test.cognitiveservices.azure.com/language/:query-knowledgebases?projectName=customer-faq-app&api-version=2021-10-01&deploymentName=production";
// var key = "0901bc24a85a4cdb9b2db9eb5ff1f510";
//
// // Create client
// // var client = new ChatClient(new Uri(endpoint), new CommunicationTokenCredential(key));
//
// var request = new KnowledgeBaseRequest
// {
//     Top = 3,
//     Question = "How to book a flight ?",
//     IncludeUnstructuredSources = true,
//     ConfidenceScoreThreshold = 0.7F,
//     AnswerSpanRequest = new AnswerSpanRequest
//     {
//         Enable = true,
//         TopAnswersWithSpan = 1,
//         ConfidenceScoreThreshold = 0.7F
//     }
// };
//
// // var question = "{ 'question': 'How to book a flight ?', 'top': 3 }";
//
// using var client = new HttpClient();
//
// // Set the subscription key and Content-Type
// client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
// HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
//
// // Make the POST request
// HttpResponseMessage response = await client.PostAsync(endpoint, content);
//
// // Read and output the response
// string result = await response.Content.ReadAsStringAsync();
// var test = JsonSerializer.Deserialize<KnowledgeBaseAnswerResponse>(result);
//
// Console.WriteLine("Result: " + result);

// Create chat thread