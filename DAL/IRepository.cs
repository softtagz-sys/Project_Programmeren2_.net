using System.Collections;
using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.DAL;

public interface IRepository
{
    public Card ReadCard(int id);
    public IEnumerable<Card> ReadAllCards();
    public IEnumerable<Card> ReadCardsOfType(CardType type);
    public void CreateCard(Card card);
    
    public Deck ReadDeck(int id);
    public IEnumerable<Deck> ReadAllDecks();
    public IEnumerable<Deck> ReadDeckByNameAndCreationDate(String name, DateTime creationDate);
    public IEnumerable<Deck> ReadAllDecksWithCards();
    public IEnumerable<Deck> ReadCardsOfDeck(long deckId);
    public void CreateDeck(Deck deck);
    public void CreateDeckEntry(DeckEntry deckEntry);
    void DeleteDeckEntry(long cardId, long deckId);
    
    public Set ReadSet(int id);
    public IEnumerable<Set> ReadAllSets();
    public IEnumerable<Set> ReadAllSetsWithCard();
    public IEnumerable<Set> ReadCardsOfSet(long setId);
    public void CreateSet(Set set);
    public void CreateSetEntry(SetEntry setEntry);
    void DeleteSetEntry(long cardId, long setId);

}