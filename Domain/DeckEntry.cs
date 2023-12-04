using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class DeckEntry
{
    public DeckEntry() { }

    public DeckEntry(Card card, Deck deck, int quantity, DateTime dt)
    {
        Card = card;
        Deck = deck;
        Quantity = quantity;
        AddedOn = dt;
    }

    public Card Card { get; set; }
    public Deck Deck { get; set; }
    
    [Key]
    public int DeckEntryId { get; set; }
    
    [Range(1,4)]
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}
