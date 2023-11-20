namespace ConsoleApp.BusinessLayer;

public static class Service
{
    public static (bool IsSuccess, string? Message) Validate(string? input)
    {
        if (input is null || input.Length > 10)
        {
            return (false, "Input is invalid");
        }

        return (true, null);
    }
}
