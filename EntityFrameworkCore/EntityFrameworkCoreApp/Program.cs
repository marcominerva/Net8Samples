using EntityFrameworkCoreApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using var db = new ApplicationDbContext();

//var people = await db.Database.SqlQueryRaw<Person>("""
//    SELECT Id, FirstName + ' ' + LastName AS Name FROM People
//    """)
//    .OrderBy(p=>p.Name)
//    .ToListAsync();

var city = "Taggia";
var people = await db.Database.SqlQuery<Person>($"""
    SELECT p.Id, FirstName + ' ' + LastName AS Name
    FROM People p
    INNER JOIN Cities c
    ON p.CityId = c.Id
    WHERE c.Name = {city}
    """)

    .OrderBy(p => p.Name).ToListAsync();

Console.ReadLine();

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Constants.ConnectionString);
        optionsBuilder.LogTo(Console.WriteLine, [RelationalEventId.CommandExecuted]);
    }
}

public record class Person(Guid Id, string Name);