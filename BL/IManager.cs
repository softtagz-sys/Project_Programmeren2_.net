using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.BL;

public interface IManager
{
    public Card GetCard(int id);
    public IEnumerable<Card> GetAllCards();
    public IEnumerable<Card> GetCardsOfType(CardType type);
    public Card getCardWithDecks(int id);
    public Card getCardWithSets(int id);
    public Card getCardWithSetsAndDecks(int id);
    public Card AddCard(string name, CardType type, CardAbility? cardAbilities, CardColour cardColours, int manaCost,
        double price, string description, bool isFoil);
    
    public Deck GetDeck(int id);
    public IEnumerable<Deck> GetAllDecks();
    public IEnumerable<Deck> GetDeckByNameAndCreationDate(String name, DateTime creationDate);
    public IEnumerable<Deck> GetAllDecksWithCards();
    public Deck AddDeck(string name, List<Card> cards, DateTime creationDate, string notes);
    public DeckEntry AddDeckEntry(Deck deck, Card card, int amount, DateTime creationDate);
    public void RemoveDeckEntry(long cardId, long deckId);
    
    public Set GetSet(int id);
    public IEnumerable<Set> GetAllSets();
    public IEnumerable<Set> GetAllSetsWithCard();
    public Set AddSet(String name, String code, DateTime releaseDate);
    public SetEntry AddSetEntry(Card card, Set set, DateTime addedOn);
    public void RemoveSetEntry(long cardId, long setId);
}