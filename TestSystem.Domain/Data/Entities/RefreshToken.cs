namespace TestSystem.Domain.Data.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
    }
}
