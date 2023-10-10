using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class Card
{
    public Card(string name, CardType type, List<CardAbility> cardAbilities, List<CardColour> cardColours, int manaCost,
        double price, string description, bool isFoil)
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
    }
    private static int _cardId = 1;
    public int Id { get; set; }
    public string Name { get; set; }
    public CardType Type { get; set; }
    public ICollection<CardAbility> CardAbilities { get; set; }
    public ICollection<CardColour> CardColours { get; set; }
    public int ManaCost { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFoil { get; set; }
}