using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class Card
{
    
    public Card()
    {
        
    }
    public Card(string name, CardType type, List<CardAbility> cardAbilities, List<CardColour> cardColours, int manaCost,
        double price, string description, bool isFoil, List<DeckEntry> deck = null, List<SetEntry> set = null)
    {
        Name = name;
        Type = type;
        CardAbilities = cardAbilities;
        CardColours = cardColours;
        ManaCost = manaCost;
        Price = price;
        Description = description;
        IsFoil = isFoil;
        Id = _cardId++;
        Decks = deck ?? new List<DeckEntry>();
        Sets = set ?? new List<SetEntry>();
    }
    private static int _cardId = 1;
    public int Id { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Name must be at least 1 character long")]
    public string Name { get; set; }
    public CardType Type { get; set; }
    public ICollection<DeckEntry> Decks { get; set; }
    public ICollection<SetEntry> Sets { get; set; }
    public ICollection<CardAbility> CardAbilities { get; set; }
    public ICollection<CardColour> CardColours { get; set; }
    public int ManaCost { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double Price { get; set; }
    public string? Description { get; set; }
    public bool IsFoil { get; set; }
}