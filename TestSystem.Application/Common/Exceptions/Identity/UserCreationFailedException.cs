using Microsoft.AspNetCore.Identity;

namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class UserCreationFailedException : Exception
    {
        public UserCreationFailedException(IEnumerable<IdentityError> errors)
        : base($"Failed to create user: {string.Join(", ", errors.Select(e => e.Description))}")
        {
        }
    }
}
