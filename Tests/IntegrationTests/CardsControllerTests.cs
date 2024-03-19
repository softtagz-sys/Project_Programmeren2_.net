using MTGM.UI.MVC.Models.Dto;
using Newtonsoft.Json;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class CardsControllerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public CardsControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetAllCards_ReturnsCorrectData()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/cards");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var responseString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<List<CardDto>>(responseString);
        Assert.NotNull(data);
    }
}