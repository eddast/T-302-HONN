using Xunit;

namespace VideotapesGalore.IntegrationTests.Context
{
    [CollectionDefinition("Test Context Collection")]
    public class TestContext : IClassFixture<TestsContextFixture>
    {
        // Intentionally left empty
    }
}