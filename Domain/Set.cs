using System.ComponentModel.DataAnnotations.Schema;
using MagicTheGatheringManagement.Domain;
namespace MTGM.BL.Domain;

public class Set
{
    public Set()
    {
        
    }
    public Set(int id, string name, String code, DateTime releaseDate, ICollection<Card> cards)
    {
        Id = id;
        Name = name;
        Code = code;
        ReleaseDate = releaseDate;
        Cards = cards;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public String Code { get; set; }
    public DateTime ReleaseDate { get; set; }
    [NotMapped]
    public ICollection<Card> Cards { get; set; }
}