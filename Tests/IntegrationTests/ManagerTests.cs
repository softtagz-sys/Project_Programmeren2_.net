using MagicTheGatheringManagement.Domain;
using MTGM.BL;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public ManagerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void AddCard_Should_Call_Create_Card_And_Return_Correct_Card()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();

        // Act
        var result = manager.AddCard("Test Card", CardType.Creature, CardAbility.Flying, 
            CardColour.Blue, 3, 1.99, "Test Description", false, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Card", result.Name);
        Assert.Equal(CardType.Creature, result.Type);
        Assert.Equal(CardAbility.Flying, result.CardAbility);
        Assert.Equal(CardColour.Blue, result.CardColour);
        Assert.Equal(3, result.ManaCost);
        Assert.Equal(1.99, result.Price);
        Assert.Equal("Test Description", result.Description);
        Assert.False(result.IsFoil);
        Assert.Null(result.User);
    }
    
    [Fact]
    public void GetCardsOfType_Should_Return_Correct_Cards()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        // Act
        var result = manager.GetCardsOfType(CardType.Creature);
        
        // Assert
        Assert.NotNull(result);
        Assert.All(result, card => Assert.Equal(CardType.Creature, card.Type));
    }
    
    
}