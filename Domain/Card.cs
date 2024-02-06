using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class Card
{
    
    public Card()
    {
        
    }
    public Card(string name, CardType type, CardAbility? cardAbility, CardColour cardColour, int manaCost,
        double price, string description, bool isFoil, List<DeckEntry> deck = null, List<SetEntry> set = null)
    {
        Name = name;
        Type = type;
        CardAbility = cardAbility;
        CardColour = cardColour;
        ManaCost = manaCost;
        Price = price;
        Description = description;
        IsFoil = isFoil;
        DeckEntries = deck ?? new List<DeckEntry>();
        SetEntries = set ?? new List<SetEntry>();
    }
    public int Id { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Name must be at least 1 character long")]
    public string Name { get; set; }
    public CardType Type { get; set; }
    public ICollection<DeckEntry> DeckEntries { get; set; }
    public ICollection<SetEntry> SetEntries { get; set; }
    public CardAbility? CardAbility { get; set; }
    public CardColour CardColour { get; set; }
    public int ManaCost { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFoil { get; set; }
}