using System.ComponentModel.DataAnnotations.Schema;
using MagicTheGatheringManagement.Domain;
namespace MTGM.BL.Domain;

public class Set
{
    public Set()
    {
        
    }
    public Set(String name, String code, DateTime releaseDate, List<SetEntry> cards = null)
    {
        Id = _setId++;
        Name = name;
        Code = code;
        ReleaseDate = releaseDate;
        Cards = cards ?? new List<SetEntry>();
    }
    
    private static int _setId = 1;
    public int Id { get; set; }
    public string Name { get; set; }
    public String Code { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<SetEntry> Cards { get; set; }
}