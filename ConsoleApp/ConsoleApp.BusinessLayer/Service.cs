global using Result = (bool IsSuccess, string Message);

namespace ConsoleApp.BusinessLayer;

public static class Service
{
    public static Result Ok()
    {
        var result = (true, "OK");
        return result;
    }

    public static Result Fail()
    {
        var result = (false, "Something terrible has happened");
        return result;
    }
}
