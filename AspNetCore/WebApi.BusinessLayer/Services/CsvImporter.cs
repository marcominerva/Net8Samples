using WebApi.BusinessLayer.Models;
using WebApi.BusinessLayer.Services.Interfaces;

namespace WebApi.BusinessLayer.Services;

public class CsvImporter : IFileImporter
{
    public async Task<IEnumerable<Person>> ImportAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        var people = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Skip(1)
            .Select(line =>
            {
                var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return new Person(parts.ElementAtOrDefault(0), parts.ElementAtOrDefault(1), parts.ElementAtOrDefault(2));
            });

        return people;
    }
}
