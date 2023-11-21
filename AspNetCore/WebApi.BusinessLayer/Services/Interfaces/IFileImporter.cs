using WebApi.BusinessLayer.Models;

namespace WebApi.BusinessLayer.Services.Interfaces;

public interface IFileImporter
{
    Task<IEnumerable<Person>> ImportAsync(Stream stream);
}