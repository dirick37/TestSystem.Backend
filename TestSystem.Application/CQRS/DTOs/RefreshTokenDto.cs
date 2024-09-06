namespace TestSystem.Application.CQRS.DTOs
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
