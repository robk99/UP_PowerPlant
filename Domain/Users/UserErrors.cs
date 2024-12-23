using Shared;

namespace Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFoundByEmail(string email) => 
            Error.NotFound($"The user with the email = '{email}' was not found.");

        public static Error WrongPassword(string email) =>
            Error.NotFound("The password you provided is not correct.");

        public static Error EmailNotUnique(string email) =>
            Error.Conflict($"User with an email: '{email}' already exists.");
    }
}
