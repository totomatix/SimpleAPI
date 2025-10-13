using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleAPI;
using SimpleAPI.Controllers;
using TestsIntegrations.Fixtures;

namespace Tests;

public class SimpleAPITests : AbstractIntegrationTest
{
    public SimpleAPITests(APIWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetAllBooks()
    {
        // Arrange

        // Act 
        var response = await _client.GetFromJsonAsync<IEnumerable<Book>>("/books/");

        // Assert
        Assert.NotEmpty(response);
    }
}
