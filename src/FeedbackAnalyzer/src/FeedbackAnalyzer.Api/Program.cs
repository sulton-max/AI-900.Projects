using FeedbackAnalyzer.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();

// app.Run();

// var apiKey = "0901bc24a85a4cdb9b2db9eb5ff1f510";
// var endpoint = "https://max-lang-test.cognitiveservices.azure.com/";
//
// // Write feedback with some personal information in english, with some french at the end
// var feedback =
//     "The food was excellent and the service was fantastic. The waiter was very friendly and the food was delicious. However, the restaurant was a bit too expensive. Je ne suis pas satisfait du tout. Le repas était délicieux, mais le service était lent.";
//
// var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
//
// // Detect language
// var language = await client.DetectLanguageAsync(feedback);
//
// // Found key phrases
// var keyPhrases = await client.ExtractKeyPhrasesAsync(feedback);
//
// // Mine opinion
// var opinion = await client.AnalyzeSentimentAsync(feedback);
// var test = opinion.Value.Sentiment
//
// // Recognize entities
// // var entities = await client.RecognizeEntitiesAsync(feedback);
//
// // Recognize personal information
// var pii = await client.RecognizePiiEntitiesAsync(feedback);
//
// var negativeWeight = 0.83; // Midpoint of negative range
// var neutralWeight = 2.5;   // Midpoint of neutral range
// var positiveWeight = 4.17; // Midpoint of positive range
//
// var rating = (opinion.Value.ConfidenceScores.Negative * negativeWeight) + 
//                 (opinion.Value.ConfidenceScores.Neutral * neutralWeight) + 
//                 (opinion.Value.ConfidenceScores.Positive * positiveWeight);
//
// Console.ReadLine();