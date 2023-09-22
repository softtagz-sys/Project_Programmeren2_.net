namespace MagicTheGatheringManagement.Domain;

public class Set
{
    public Set(string name, int year, string description)
    {
        Name = name;
        Year = year;
        Description = description;
    }
    public string Name { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"Set: {Name}, Year: {Year}, Description: {Description}";
    }
}