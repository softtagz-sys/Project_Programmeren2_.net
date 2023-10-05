using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.DAL;

public interface IRepository
{
    public Card ReadCard(int id);
    public List<Card> ReadAllCards();
    public List<Card> ReadCardsOfType(CardType type);
    public void CreateCard(Card card);
    
    public Deck ReadDeck(int id);
    public List<Deck> ReadAllDecks();
    public List<Deck> ReadDeckByNameAndCreationDate(Card card);
    public void CreateDeck(Deck deck);
    
}