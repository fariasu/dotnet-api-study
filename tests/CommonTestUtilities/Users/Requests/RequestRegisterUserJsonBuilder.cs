﻿using Bogus;
using TaskManager.Communication.DTOs.Users.Requests;

namespace CommonTestUtilities.Users.Requests;

public static class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "Aa1@"));
    }
}