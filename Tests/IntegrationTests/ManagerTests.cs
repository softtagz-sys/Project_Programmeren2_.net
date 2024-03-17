using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement;
using MagicTheGatheringManagement.Domain;
using Microsoft.AspNetCore.Identity;
using MTGM.BL;
using MTGM.BL.Domain;
using MTGM.DAL;
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
        var user = new IdentityUser();
        var card = new Card("Test Card", CardType.Creature, CardAbility.Flying, CardColour.Blue, 3, 1.99, "Test Description", false, user);

        // Act
        var result = manager.AddCard("Test Card", CardType.Creature, CardAbility.Flying, CardColour.Blue, 3, 1.99, "Test Description", false, user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(card.Name, result.Name);
        Assert.Equal(card.Type, result.Type);
        Assert.Equal(card.CardAbility, result.CardAbility);
        Assert.Equal(card.CardColour, result.CardColour);
        Assert.Equal(card.ManaCost, result.ManaCost);
        Assert.Equal(card.Price, result.Price);
        Assert.Equal(card.Description, result.Description);
        Assert.Equal(card.IsFoil, result.IsFoil);
        Assert.Equal(card.User, result.User);
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