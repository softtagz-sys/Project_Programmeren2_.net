using MTGM.BL.Domain;

namespace MagicTheGatheringManagement.Extensions;

public static class CardExtentions
{
    public static string GetString(this Card card)
    {
        string cardBorder = "-----------------------";
        string nameLine = $"| {card.Name.ToUpper()}";
        string typeLine = $"| Type: {card.Type}";
        string manaCostLine = $"| ManaCost: {card.ManaCost}";
        string priceLine = $"| Price: {card.Price}";
        string editionLine = $"| {(card.IsFoil ? "Foil edition" : "Regular edition")}";

        string cardText = $"\n+{cardBorder} +\n{nameLine,-24} |\n{typeLine,-24} |\n{manaCostLine,-24} |\n{priceLine,-24} |\n{editionLine,-24} |\n+{cardBorder} +";

        return cardText;
    }
}