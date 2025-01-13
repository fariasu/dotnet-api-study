using FluentValidation;
using TaskManager.Communication.DTOs.Request;

namespace TaskManager.Application.Validators.Task;

public class TaskValidator : AbstractValidator<RequestCreateTaskJson>
{
    public TaskValidator()
    {
        RuleFor(task => task.Title).NotEmpty()
            .WithMessage("Title is required.");
        RuleFor(task => task.Description).NotEmpty()
            .WithMessage("Description is required.");
        RuleFor(task => task.EndDate).NotEmpty()
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Date is required and can't be in the past.");
        RuleFor(task => task.TaskPriority).NotNull().IsInEnum()
            .WithMessage("Priority is required.");
        RuleFor(task => task.TaskStatus).NotNull().IsInEnum()
            .WithMessage("Status is required.");
    }
}