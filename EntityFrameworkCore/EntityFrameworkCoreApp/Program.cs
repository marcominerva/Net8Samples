// See https://aka.ms/new-console-template for more information
using EntityFrameworkCoreApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using var db = new ApplicationDbContext();

Console.ReadLine();

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Constants.ConnectionString);
        optionsBuilder.LogTo(Console.WriteLine, [RelationalEventId.CommandExecuted]);
    }
}

public record class Person(Guid Id, string Name);