using System;

namespace MagicTheGatheringManagement.Domain;

public class Set
{
    public Set(string name, String code, DateTime releaseDate)
    {
        Name = name;
        Code = code;
        ReleaseDate = releaseDate;
    }
    public string Name { get; set; }
    public String Code { get; set; }
    public DateTime ReleaseDate { get; set; }

    public override string ToString()
    {
        return $"\nSet: {Name}\nCode: {Code}\nRelease Date: {ReleaseDate.ToShortDateString()}";
    }

}