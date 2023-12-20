using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;
using MTGM.DAL;

namespace MTGM.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;

    public Manager(IRepository repository)
    {
        _repository = repository;
    }
    
    public Card GetCard(int id)
    {
        return _repository.ReadCard(id);
    }

    public IEnumerable<Card> GetAllCards()
    {
        return _repository.ReadAllCards();
    }

    public IEnumerable<Card> GetCardsOfType(CardType type)
    {
        return _repository.ReadCardsOfType(type);
    }

    public Card getCardWithDecks(int id)
    {
        return _repository.ReadCardWithDecks(id);
    }

    public Card getCardWithSets(int id)
    {
        return _repository.ReadCardWithSets(id);
    }

    public Card getCardWithSetsAndDecks(int id)
    {
        return _repository.ReadCardWithDecksAndSets(id);
    }

    public Card AddCard(string name, CardType type, CardAbility? cardAbilities, CardColour cardColours, int manaCost,
        double price,
        string description, bool isFoil)
    {
        Card card = new Card(name, type, cardAbilities, cardColours, manaCost, price, description, isFoil);
        ValidateObject(card);
        _repository.CreateCard(card);
        return card;
    }

    public Deck GetDeck(int id)
    {
        return _repository.ReadDeck(id);
    }

    public IEnumerable<Deck> GetAllDecks()
    {
        return _repository.ReadAllDecks();
    }

    public IEnumerable<Deck> GetDeckByNameAndCreationDate(string name, DateTime creationDate)
    {
        return _repository.ReadDeckByNameAndCreationDate(name, creationDate);
    }

    public IEnumerable<Deck> GetAllDecksWithCards()
    {
        return _repository.ReadAllDecksWithCards();
    }

    public Deck AddDeck(string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Deck deck = new Deck(name, creationDate, notes);
        ValidateObject(deck);
        _repository.CreateDeck(deck);
        return deck;
    }

    public DeckEntry AddDeckEntry(Deck deck, Card card, int amount, DateTime creationDate)
    {
        DeckEntry deckEntry = new DeckEntry(card, deck, amount, creationDate);
        ValidateObject(deckEntry);
        _repository.CreateDeckEntry(deckEntry);
        return deckEntry;
    }

    public void RemoveDeckEntry(long cardId, long deckId)
    {
        _repository.DeleteDeckEntry(cardId, deckId);
    }

    public Set GetSet(int id)
    {
        return _repository.ReadSet(id);
    }

    public IEnumerable<Set> GetAllSets()
    {
        return _repository.ReadAllSets();
    }

    public IEnumerable<Set> GetAllSetsWithCard()
    {
        return _repository.ReadAllSetsWithCard();
    }

    public Set AddSet(string name, string code, DateTime releaseDate)
    {
        Set set = new Set(name, code, releaseDate);
        ValidateObject(set);
        _repository.CreateSet(set);
        return set;
    }

    public SetEntry AddSetEntry(Card card, Set set, DateTime addedOn)
    {
        SetEntry setEntry = new SetEntry(card, set, addedOn);
        ValidateObject(setEntry);
        _repository.CreateSetEntry(setEntry);
        return setEntry;
    }

    public void RemoveSetEntry(long cardId, long setId)
    {
        _repository.DeleteSetEntry(cardId, setId);
    }

    private void ValidateObject(Object o)
    {
        var validationContext = new ValidationContext(o);
        Validator.ValidateObject(o, validationContext, true);
    }
}