using FluentValidation;
using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.Validators.Users;

public class UpdatePasswordValidator : AbstractValidator<RequestUpdatePasswordJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUpdatePasswordJson>());
    }
}