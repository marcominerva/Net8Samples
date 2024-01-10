using Intero = int;
using Price = double;

Intero TheAnswer = 42;
Price price = 12.4;

Position position = (12, 54.4324);

var person = new Person("Marco", "Minerva");


#region Collection Initializer

IEnumerable<int> listOfInt = [1, 2, 3, 4, 5, 6];
List<string> listOfString = ["Marco", "Donald Duck", "Mikey Mouse" ];

PrintValues(listOfInt);

var date = DateOnly.ParseExact("29/11/2023", [ "dd/MM/yyyy", "dd-MM-yyyy" ], System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
Console.WriteLine(date);

void PrintValues(IEnumerable<int> list)
{
    Console.WriteLine(list.GetType().FullName);

    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}

#endregion

public record class PersonRecord(string FirstName, string LastName);

public class Person(string firstName, string lastName)
{
    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName;
}

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<string> Tags { get; set; } = [];
}