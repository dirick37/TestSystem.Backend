namespace TestSystem.Application.Common.Exceptions.Identity
{
    public class UserIdNotFoundException : Exception
    {
        public UserIdNotFoundException(string id)
           : base($"User with Id {id} not  found.") { }
    }
}
