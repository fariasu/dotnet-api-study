using Bogus;
using TaskManager.Communication.DTOs.Users.Requests;

namespace CommonTestUtilities.Requests;

public static class RequestUpdatePasswordJsonBuilder
{
    public static RequestUpdatePasswordJson Build()
    {
        return new Faker<RequestUpdatePasswordJson>()
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "@1aA"));
    }
}