using Xunit;

namespace VideotapesGalore.IntegrationTests
{
    [CollectionDefinition("Test Context Collection")]
    public class TestContext : IClassFixture<TestsContextFixture>
    {
        
    }
}