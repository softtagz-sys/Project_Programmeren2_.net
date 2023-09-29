using System;
using System.Collections.Generic;
using System.Text;

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
        StringBuilder cardList = new StringBuilder();

        foreach (Card card in Cards)
        {
            cardList.Append(card.Name + ", ");
        }

        string formattedCreationDate = CreationDate.ToShortDateString();

        return $"\nDeck Name: {Name}\nCreated on: {formattedCreationDate}\nContains: {cardList}\nNotes: {Notes}";
    }

}