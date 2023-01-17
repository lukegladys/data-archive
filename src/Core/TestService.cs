using ConfigureCount;
using Microsoft.Extensions.Options;

namespace Core;

public interface ITestService
{
    public Task<int> GetCount();
}

public class TestService : ITestService
{
    private readonly IOptions<DataArchiveOptions> _dataArchiveOptions;

    public TestService(IOptions<DataArchiveOptions> dataArchiveOptions)
    {
        _dataArchiveOptions = dataArchiveOptions;
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(100);
    }
}