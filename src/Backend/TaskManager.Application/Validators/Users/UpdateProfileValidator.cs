using FluentValidation;
using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.Validators.Users;

public class UpdateProfileValidator : AbstractValidator<RequestUpdateProfileJson>
{
    public UpdateProfileValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage("Name is required.");
    }
}