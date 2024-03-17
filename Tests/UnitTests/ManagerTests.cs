using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement.Domain;
using Moq;
using MTGM.BL;
using MTGM.BL.Domain;
using MTGM.DAL;

namespace Tests.UnitTests;

public class ManagerTests
{
    private readonly IManager _manager;
    private readonly Mock<IRepository> _repositoryMock;

    public ManagerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _manager = new Manager(_repositoryMock.Object);
    }

    [Fact]
    public void AddCard_Should_Reject_Invalid_Card()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.ReadCard(123))
            .Returns((Card)null);

        // Act
        var action = () => _manager.AddCard(null, CardType.Creature, null, CardColour.Black, -1, -1, null, false, null);

        // Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock.Verify(
            repo => repo.CreateCard(It.IsAny<Card>()),
            Times.Never);
    }

    [Fact]
    public void AddCard_Should_Persist_When_Valid_Card_Supplied()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.ReadCard(123))
            .Returns(new Card
            {
                Id = 123
            });

        // Act
        var card = _manager.AddCard("CardName", CardType.Creature, null, CardColour.Black, 3, 3.5, "Description", false,
            null);

        // Assert
        _repositoryMock.Verify(
            repo => repo.CreateCard(It.IsAny<Card>()),
            Times.Once);
    }
}