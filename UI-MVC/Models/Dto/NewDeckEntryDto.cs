using System.ComponentModel.DataAnnotations;
using MTGM.BL.Domain;

namespace MTGM.UI.MVC.Models.Dto;

public class NewDeckEntryDto
{
    public int CardId { get; set; }
    public int DeckId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}