using System.Collections.Generic;

namespace MagicTheGatheringManagement.Domain;

public class Card
{
    public Card(string name, CardType type, List<CardAbility> cardAbilities, List<CardColour> cardColours, int manaCost, double price, string description, bool isFoil)
    {
        Name = name;
        Type = type;
        CardAbilities = cardAbilities;
        CardColours = cardColours;
        ManaCost = manaCost;
        Price = price;
        Description = description;
        IsFoil = isFoil;
    }

    public string Name { get; set; }
    public CardType Type { get; set; }
    public List<CardAbility>? CardAbilities { get; set; }
    public List<CardColour> CardColours { get; set; }
    public int ManaCost { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFoil { get; set; }

    public override string ToString()
    {
        string cardBorder = "--------------------------";
        string nameLine = $"| {Name.ToUpper(),-22} |";
        string typeLine = $"| Type: {Type,-22} |";
        string manaCostLine = $"| ManaCost: {ManaCost,-22} |";
        string priceLine = $"| Price: {Price,-22} |";
        string editionLine = $"| {(IsFoil ? "Foil edition" : "Regular edition"),-22} |";

        string card = $"\n{cardBorder}\n{nameLine}\n{typeLine}\n{manaCostLine}\n{priceLine}\n{editionLine}\n{cardBorder}";

        return card;
    }


}