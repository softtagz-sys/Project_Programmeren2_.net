using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class HomeControllerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public HomeControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Index_Returns_Correct_View()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}