using Xunit;

namespace VideotapesGalore.IntegrationTests.Context
{
    /// <summary>
    /// This sets a new collection that uses the configured TestsContextFixture
    /// All tests that are marked with the same collection use the same static context
    /// </summary>
    [CollectionDefinition("Test Context Collection")]
    public class TestContext : IClassFixture<TestsContextFixture> { }
}