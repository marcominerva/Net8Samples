using ClosedXML.Excel;
using WebApi.BusinessLayer.Models;
using WebApi.BusinessLayer.Services.Interfaces;

namespace WebApi.BusinessLayer.Services;

public class ExcelImporter : IFileImporter
{
    public Task<IEnumerable<Person>> ImportAsync(Stream stream)
    {
        using var excel = new XLWorkbook(stream);
        var worksheet = excel.Worksheets.First();

        var people = worksheet.RowsUsed().Skip(1).Select(row =>
        {
            var firstName = row.Cell(1).Value.ToString();
            var lastName = row.Cell(2).Value.ToString();
            var city = row.Cell(3).Value.ToString();

            return new Person(firstName, lastName, city);
        }).ToList();

        return Task.FromResult(people.AsEnumerable());
    }
}
