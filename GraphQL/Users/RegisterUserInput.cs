namespace BlogocomApiV2.GraphQL.Users
{
    public record RegisterUserInput(
        string? FirstName,
        string Phone,
        string? Email,
        string Password
        );
}
