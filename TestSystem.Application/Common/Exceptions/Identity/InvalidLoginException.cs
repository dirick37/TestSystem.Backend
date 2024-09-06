namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException()
           : base($"Invalid login attempt.") { }
    }
}
