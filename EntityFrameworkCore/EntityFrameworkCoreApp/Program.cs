using EntityFrameworkCoreApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using var db = new ApplicationDbContext();

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