using Microsoft.AspNetCore.Mvc;
using Moq;
using MTGM.BL;
using MTGM.BL.Domain;
using MTGM.UI.MVC.Controllers.Api;

namespace Tests.UnitTests;

public class DeckEntriesControllerTests
{
    private readonly Mock<IManager> _managerMock;
    private readonly DeckEntriesController _controller;

    public DeckEntriesControllerTests()
    {
        _managerMock = new Mock<IManager>();
        _controller = new DeckEntriesController(_managerMock.Object);
    }

    [Fact]
    public void GetOneDeckEntry_ReturnsNotFound_WhenDeckEntryDoesNotExist()
    {
        // Arrange
        _managerMock.Setup(m => m.getDeckEntry(It.IsAny<int>())).Returns((DeckEntry)null);

        // Act
        var result = _controller.GetOneDeckEntry(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
    
    [Fact]
    public void GetCardsOfDeckEntries_ReturnsNoContent_WhenDeckEntriesDoNotExist()
    {
        // Arrange
        _managerMock.Setup(m => m.GetDeckEntriesOfDeck(It.IsAny<int>())).Returns(new List<DeckEntry>());

        // Act
        var result = _controller.GetCardsOfDeckEntries(1);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }
}