using System.ComponentModel.DataAnnotations;

namespace MTGM.BL.Domain;

public class SetEntry
{
    public Card Card { get; set; }
    public Set Set { get; set; }

    public SetEntry(Card card, Set set, DateTime addedOn)
    {
        Card = card;
        Set = set;
        AddedOn = addedOn;
    }

    [Key]
    public int SetEntryId { get; set; }
    public DateTime AddedOn { get; set; }
}