//using System.Globalization;
//using ConsoleApp.BusinessLayer;

//var date = DateOnly.ParseExact("29/11/2023", ["dd/MM/yyyy", "dd-MM-yyyy"], CultureInfo.InvariantCulture, DateTimeStyles.None);
//Console.WriteLine(date);

//IList<double> list = [1, 2.3, 3, 3];
//list.Add(12);
//var list2 = list.AsReadOnly();
//list.Add(1);

//Foo(list);

//TimeProvider timeProvider = TimeProvider.System;

//Microsoft.Extensions.Time.Testing.FakeTimeProvider faker = new Microsoft.Extensions.Time.Testing.FakeTimeProvider();
//faker.SetUtcNow(new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero));

//Console.WriteLine(faker.GetUtcNow().ToString());
//faker.SetUtcNow(new DateTimeOffset(2020, 1, 1, 1, 1, 2, TimeSpan.Zero));
//Console.WriteLine(faker.GetUtcNow().ToString());

//Result result = Service.Ok();
//Console.WriteLine(result.Message);

//void Foo(IList<double> list)
//{

//}

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}

