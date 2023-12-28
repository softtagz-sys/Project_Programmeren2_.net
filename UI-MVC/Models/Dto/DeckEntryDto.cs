using System.ComponentModel.DataAnnotations;
using MTGM.BL.Domain;

namespace MTGM.UI.MVC.Models.Dto;

public class DeckEntryDto
{
    public string Card { get; set; }
    public string Deck { get; set; }
    
    [Range(1,4)]
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}