namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException()
             : base($"Invalid role.") { }
    }
}
