using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeAllUsers
{
    public class RevokeAllUsersCommand : IRequest<string>
    {

    }
}
