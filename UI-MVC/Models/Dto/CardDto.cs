using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.UI.MVC.Models.Dto;

public class CardDto
{
    public int Id { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Name must be at least 1 character long")]
    public string Name { get; set; }
    public CardType Type { get; set; }
    public ICollection<DeckEntry> DeckEntries { get; set; }
    public ICollection<SetEntry> SetEntries { get; set; }
    public CardAbility? CardAbility { get; set; }
    public CardColour CardColour { get; set; }
    public int ManaCost { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double Price { get; set; }
    public string? Description { get; set; }
    public bool IsFoil { get; set; }
}