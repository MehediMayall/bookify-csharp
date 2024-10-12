namespace Bookify.Domain;

public static class UserErrors
{
    public static Error NotFound(Guid Id) => new("User.NotFound", "User not found with the user Id: " + Id);
}