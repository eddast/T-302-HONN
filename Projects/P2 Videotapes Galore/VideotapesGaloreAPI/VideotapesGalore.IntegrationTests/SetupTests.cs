using Microsoft.AspNetCore.Mvc.Testing;
using VideotapesGalore.WebApi;

namespace VideotapesGalore.IntegrationTests
{
    public class SetupTests
    {
        public static WebApplicationFactory<Startup> _factory { get; set; } = null;

        public static WebApplicationFactory<Startup> GetWebApplicationFactory() 
        {
            if (_factory == null) {
                _factory = new WebApplicationFactory<Startup>();
            }
            return _factory;
        }
    }
}