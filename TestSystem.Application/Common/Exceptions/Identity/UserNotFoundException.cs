namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
           : base($"User not found.") { }
    }
}
