using MagicTheGatheringManagement.Domain;
using Microsoft.AspNetCore.Identity;
using MTGM.BL.Domain;

namespace MTGM.BL;

public interface IManager
{
    public Card GetCard(int id);
    public IEnumerable<Card> GetAllCards();
    public IEnumerable<Card> GetCardsOfType(CardType type);
    public IEnumerable<Card> GetCardsFromDeck(long deckId);
    public Card GetCardWithSetsAndDecks(int id);
    public Card GetCardWithSetsAndDecksAndUsers(int id);
    public Card AddCard(string name, CardType type, CardAbility? cardAbilities, CardColour cardColours, int manaCost,
        double price, string description, bool isFoil, IdentityUser user);
    public Card UpdateCard(Card card);
    
    public Deck GetDeck(int id);
    public IEnumerable<Deck> GetAllDecks();
    public IEnumerable<Deck> GetDeckByNameAndCreationDate(string name, DateTime? creationDate);
    public IEnumerable<Deck> GetAllDecksWithCards();
    public DeckEntry GetDeckEntryWithCard(long id);
    public Deck AddDeck(string name, List<Card> cards, DateTime creationDate, string notes);
    public DeckEntry AddDeckEntry(int cardId, int deckId, int amount, DateTime creationDate);
    public void RemoveDeckEntry(long cardId, long deckId);
    
    public Set GetSet(int id);
    public IEnumerable<Set> GetAllSets();
    public IEnumerable<Set> GetAllSetsWithCard();
    public Set AddSet(String name, String code, DateTime releaseDate);
    public SetEntry AddSetEntry(Card card, Set set, DateTime addedOn);
    public void RemoveSetEntry(long cardId, long setId);
    public IEnumerable<DeckEntry> GetDeckEntriesOfDeck(long deckId);
    public DeckEntry getDeckEntry(long id);
}