using ConfigureCount;
using Microsoft.Extensions.Options;

namespace Core;

public interface ITestService
{
    public Task<string> GetValue();
}

public class TestService : ITestService
{
    private readonly IOptions<DataArchiveOptions> _dataArchiveOptions;

    public TestService(IOptions<DataArchiveOptions> dataArchiveOptions)
    {
        _dataArchiveOptions = dataArchiveOptions;
    }

    public Task<string> GetValue()
    {
        return Task.FromResult(_dataArchiveOptions.Value.TestLol);
    }
}