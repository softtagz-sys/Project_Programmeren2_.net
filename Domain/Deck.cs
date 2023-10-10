using System.Diagnostics.Metrics;
using System.Text;
using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class Deck
{
    public Deck(string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Name = name;
        Cards = cards;
        CreationDate = creationDate;
        Notes = notes;
        Id = _deckId++;
    }
    private static int _deckId = 1;
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Card> Cards { get; set; }
    public DateTime CreationDate { get; set; }
    public string Notes { get; set; }
}