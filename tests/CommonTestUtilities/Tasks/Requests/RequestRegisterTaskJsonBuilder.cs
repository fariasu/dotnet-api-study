using Bogus;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace CommonTestUtilities.Tasks.Requests;

public static class RequestRegisterTaskJsonBuilder
{
    public static RequestTaskJson Build()
    {
        return new Faker<RequestTaskJson>()
            .RuleFor(task => task.Title, faker => faker.Lorem.Sentence(20))
            .RuleFor(task => task.Description, faker => faker.Lorem.Sentence(100))
            .RuleFor(task => task.TaskPriority, faker => faker.PickRandom<TaskPriority>())
            .RuleFor(task => task.TaskStatus, faker => faker.PickRandom<TaskStatus>())
            .RuleFor(task => task.EndDate, faker => faker.Date.Future());
    }
}