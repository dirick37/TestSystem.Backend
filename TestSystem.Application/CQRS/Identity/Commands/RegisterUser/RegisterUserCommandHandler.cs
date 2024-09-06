using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;
using TestSystem.Application.Interfaces;
using TestSystem.Domain.Data.Models;
using TestSystem.Domain.Data.Entities;
using TestSystem.Application.Common.Exceptions.Identity;

namespace TestSystem.Application.CQRS.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ITestSystemDbContext _context;

        public RegisterUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITestSystemDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new UserAlreadyExistsException();
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };

            var roleName = string.IsNullOrEmpty(request.Role) ? "Student" : request.Role;

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                throw new InvalidRoleException();
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationFailedException(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, roleName);

            switch (roleName)
            {
                case Roles.Administrator:
                    var administrator = new Administrator { Id = Guid.NewGuid(), UserId = user.Id };
                    _context.Administrators.Add(administrator);
                    break;
                case Roles.Teacher:
                    var teacher = new Teacher { Id = Guid.NewGuid(), UserId = user.Id };
                    _context.Teachers.Add(teacher);
                    break;
                case Roles.Student:
                    var student = new Student { Id = Guid.NewGuid(), UserId = user.Id };
                    _context.Students.Add(student);
                    break;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return "User registered successfully.";
        }
    }
}
