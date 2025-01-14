using FluentValidation;
using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.Validators.Users;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(user => user.Name).MaximumLength(255)
            .WithMessage("Name must not exceed 255 characters.");
        RuleFor(user => user.Email).NotEmpty()
            .WithMessage("Email is required.");
        RuleFor(user => user.Email).EmailAddress()
            .WithMessage("Email is invalid.");
        RuleFor(user => user.Email).MaximumLength(255)
            .WithMessage("Email must not exceed 255 characters.");
        RuleFor(x => x.Password);
        
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}