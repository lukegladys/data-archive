using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Core;

// The function handler that will be called for each Lambda event
string Handler(IteratorEvent input, ILambdaContext context) => JsonSerializer.Serialize(input);

// Build the Lambda runtime client passing in the handler to call for each
// event and the JSON serializer to use for translating Lambda JSON documents
// to .NET types.
await LambdaBootstrapBuilder.Create((Func<IteratorEvent, ILambdaContext, string>?)Handler, new DefaultLambdaJsonSerializer(options =>
    {
        options.PropertyNameCaseInsensitive = true;
        options.WriteIndented = true;
    }))
    .Build()
    .RunAsync();