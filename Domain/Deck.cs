using System;
using System.Collections.Generic;
using System.Text;
using MTGM.BL.Domain;

namespace MagicTheGatheringManagement.Domain;

public class Deck
{
    public Deck(int id, string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Id = id;
        Name = name;
        Cards = cards;
        CreationDate = creationDate;
        Notes = notes;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Card> Cards { get; set; }
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