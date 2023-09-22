namespace MagicTheGatheringManagement.Domain;

public class Card
{
    public Card(string name, CardType type, int manaCost, double price, string description, DateTime releaseDate, bool isFoil)
    {
        Name = name;
        Type = type;
        ManaCost = manaCost;
        Price = price;
        Description = description;
        ReleaseDate = releaseDate;
        IsFoil = isFoil;
    }
    public string Name { get; set; }
    public CardType Type { get; set; }
    public int ManaCost { get; set; }
    public double Price { get; set; }
    
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool IsFoil { get; set; }

    public override string ToString()
    {
        return $"Card: {Name}, Type: {Type}, ManaCost: {ManaCost}, Price: {Price:C}, ReleaseDate: {ReleaseDate.ToShortDateString()}, IsFoil: {IsFoil}";
    }
}