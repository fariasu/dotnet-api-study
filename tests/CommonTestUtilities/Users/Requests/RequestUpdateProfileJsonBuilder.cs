using Bogus;
using TaskManager.Communication.DTOs.Users.Requests;

namespace CommonTestUtilities.Users.Requests;

public static class RequestUpdateProfileJsonBuilder
{
    public static RequestUpdateProfileJson Build()
    {
        return new Faker<RequestUpdateProfileJson>()
            .RuleFor(user => user.Name, faker => faker.Person.FirstName);
    }
}