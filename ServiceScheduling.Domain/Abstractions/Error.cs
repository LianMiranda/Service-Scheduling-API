namespace ServiceScheduling.Domain.Interfaces;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "a value was not provided");
}