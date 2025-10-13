using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace SimpleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly string _connexionString;

    public BooksController(IConfiguration configuration)
    {
        // récupération de la chaîne de connexion dans la configuration
        _connexionString = configuration.GetConnectionString("DefaultConnection")!;
        // si la chaîne de connexionn'a pas été trouvé => déclenche une exception => code http 500 retourné
        if (string.IsNullOrEmpty(_connexionString))
        {
            throw new InvalidOperationException("Error : Connexion string not found ! ");
        }
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        string query = "SELECT id, isbn, titre, description FROM public.book;";
        IEnumerable<Book> books;
        using (var connexion = new NpgsqlConnection(_connexionString))
        {
            books = await connexion.QueryAsync<Book>(query);
        }
        return books;
    }
}
