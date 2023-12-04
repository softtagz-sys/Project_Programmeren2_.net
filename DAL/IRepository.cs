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
    public void CreateDeck(Deck deck);
    public void CreateDeckEntry(DeckEntry deckEntry);
    public IEnumerable<DeckEntry> ReadAllDeckEntries();
    public void CreateSet(Set set);
    public void CreateSetEntry(SetEntry setEntry);
    public IEnumerable<SetEntry> ReadAllSetEntries();

}