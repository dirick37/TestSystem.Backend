namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class InvalidOrExpiredRefreshTokenException : Exception
    {
        public InvalidOrExpiredRefreshTokenException()
          : base($"Invalid or expired refresh token.") { }
    }
}
