using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace VideotapesGalore.WebApi.Authorization
{
    /// <summary>
    /// Implements user requirement to access restricted routes via authorization requirement
    /// </summary>
    public class InitializationRequirement : IAuthorizationRequirement
    {
        public InitializationRequirement() { }
    }
}