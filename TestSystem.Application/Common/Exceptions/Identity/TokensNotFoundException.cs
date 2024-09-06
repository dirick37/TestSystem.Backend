using Microsoft.AspNetCore.Identity;

namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class TokensNotFoundException : Exception
    {
        public TokensNotFoundException()
           : base($"No tokens found in the system.") { }
    }
}
