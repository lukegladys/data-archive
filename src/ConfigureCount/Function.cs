using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using ConfigureCount;
using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var serviceProvider = Startup.ConfigureServices();

// The function handler that will be called for each Lambda event
var handler = async (ArchiveEvent input, ILambdaContext context) =>
{
    var testService = serviceProvider.GetRequiredService<ITestService>();
    var count = await testService.GetCount();

    var dataArchiveOptions = serviceProvider.GetRequiredService<IOptions<DataArchiveOptions>>().Value;
    
    var iteratorModel = new IteratorModel(count, 0, dataArchiveOptions.BatchSize, true);
    var iteratorEvent = new IteratorEvent()
    {
        IteratorModel = iteratorModel,
        Data = input
    };
    return iteratorEvent;
};

// Build the Lambda runtime client passing in the handler to call for each
// event and the JSON serializer to use for translating Lambda JSON documents
// to .NET types.
await LambdaBootstrapBuilder.Create(handler,
        new DefaultLambdaJsonSerializer(options =>
    {
        options.PropertyNameCaseInsensitive = true;
    }))
    .Build()
    .RunAsync();