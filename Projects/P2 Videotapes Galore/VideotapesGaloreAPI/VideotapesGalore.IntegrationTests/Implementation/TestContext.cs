using Xunit;

namespace VideotapesGalore.IntegrationTests.Implementation
{
    [CollectionDefinition("Test Context Collection")]
    public class TestContext : IClassFixture<TestsContextFixture>
    {
        
    }
}