int TheAnswer = 42;
























List<int> listOfInt = new List<int>() { 1, 2, 3, 4, 5, 6 };
List<string> ListOfString = new List<string>() { "1", "2", "3", "4", "5", "6" };

PrintValues(listOfInt);

var date = DateOnly.ParseExact("29/11/2023", new[] {"dd/MM/yyyy", "dd-MM-yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
Console.WriteLine(date);

void PrintValues(IEnumerable<int> list)
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}

public class Person
{    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public record class PersonRecord(string FirstName, string LastName);

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<string> Tags { get; set; } = new List<string>();
}