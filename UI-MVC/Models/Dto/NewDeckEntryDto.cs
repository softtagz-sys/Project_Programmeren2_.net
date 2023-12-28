using System.ComponentModel.DataAnnotations;
using MTGM.BL.Domain;

namespace MTGM.UI.MVC.Models.Dto;

public class NewDeckEntryDto
{
    public Deck Card { get; set; }
    public Card Deck { get; set; }
    
    [Key]
    public int DeckEntryId { get; set; }
    
    [Range(1,4)]
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}