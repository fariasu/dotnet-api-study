using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace TaskManager.Application.Validators.Users;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string PasswordErrorMessage =
        "The password must have at least 8 letters with at least 1 uppercase, 1 lowercase, 1 number and a symbol(! @ . ? $ & # %).";
    
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        if (Regex.IsMatch(password, @"[A-Z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        if (Regex.IsMatch(password, @"[a-z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        if (Regex.IsMatch(password, @"[0-9]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        if (Regex.IsMatch(password, @"[\!\@\.\?\$\&\#\%]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, PasswordErrorMessage);
            return false;
        }

        return true;
    }
}