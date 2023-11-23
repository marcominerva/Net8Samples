int TheAnswer = 42;



#region Collection Initializer

List<int> listOfInt = new List<int>() { 1, 2, 3, 4, 5, 6 };
List<string> listOfString = new List<string>() { "Marco", "Donald Duck", "Mikey Mouse" };

PrintValues(listOfInt);

var date = DateOnly.ParseExact("29/11/2023", new[] { "dd/MM/yyyy", "dd-MM-yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
Console.WriteLine(date);

void PrintValues(IEnumerable<int> list)
{
    Console.WriteLine(list.GetType().Name);

    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}

#endregion

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

    public List<string> Tags { get; set; } = new List<string>();
}