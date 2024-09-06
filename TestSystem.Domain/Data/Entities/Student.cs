namespace TestSystem.Domain.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
