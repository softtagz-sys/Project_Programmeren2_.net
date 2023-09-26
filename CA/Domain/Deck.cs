using System;
using System.Collections.Generic;

namespace MagicTheGatheringManagement.Domain;

public class Deck
{
    public Deck(string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Name = name;
        Cards = cards;
        CreationDate = creationDate;
        Notes = notes;
    }

    public string Name { get; set; }
    public List<Card> Cards { get; set; }
    public DateTime CreationDate { get; set; }
    public string Notes { get; set; }

    public override string ToString()
    {
        return $"Deck: {Name}, CreationDate: {CreationDate.ToShortDateString()}, Notes: {Notes}";
    }
}