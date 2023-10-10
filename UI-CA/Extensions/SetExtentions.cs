using MTGM.BL.Domain;

namespace MagicTheGatheringManagement.Extensions;

public static class SetExtentions
{
    public static string GetString(this Set set)
    {
        return $"\nSet: {set.Name}\nCode: {set.Code}\nRelease Date: {set.ReleaseDate.ToShortDateString()}";
    }
}