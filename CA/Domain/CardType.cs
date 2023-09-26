namespace MagicTheGatheringManagement.Domain;

public enum CardType : byte
{
    Artifact = 1, 
    Creature, 
    Enchantment, 
    Instant, 
    Land, 
    Planeswalker, 
    Sorcery, 
}