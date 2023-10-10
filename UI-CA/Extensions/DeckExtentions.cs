using System.Text;
using MTGM.BL.Domain;

namespace MagicTheGatheringManagement.Extensions;

public static class DeckExtentions
{
    public static string GetString(this Deck deck)
    {
        StringBuilder cardList = new StringBuilder();

        foreach (Card card in deck.Cards)
        {
            cardList.Append(card.Name + ", ");
        }

        string formattedCreationDate = deck.CreationDate.ToShortDateString();

        return $"\nDeck Name: {deck.Name}\nCreated on: {formattedCreationDate}\nContains: {cardList}\nNotes: {deck.Notes}";
    }
}