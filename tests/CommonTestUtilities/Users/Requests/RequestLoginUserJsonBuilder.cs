using Bogus;
using TaskManager.Communication.DTOs.Users.Requests;

namespace CommonTestUtilities.Users.Requests;

public static class RequestLoginUserJsonBuilder
{
    public static RequestLoginUserJson Build()
    {
        return new Faker<RequestLoginUserJson>()
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "A1@a"));
    }
}