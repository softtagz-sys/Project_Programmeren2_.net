using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.DAL;

public class InMemoryRepository : IRepository
{
    private static IList<Set> _sets;
    private static IList<SetEntry> _setEntries;
    private static IList<Card> _cards;
    private static IList<DeckEntry> _deckEntries;
    private static IList<Deck> _decks;
    
    private int _cardId = 1;
    private int _deckId = 1;
    
    public InMemoryRepository()
    {
        _sets = new List<Set>();
        _setEntries = new List<SetEntry>();
        _cards = new List<Card>();
        _deckEntries = new List<DeckEntry>();
        _decks = new List<Deck>();
    }
    
    public Card ReadCard(int id)
    {
        return _cards.FirstOrDefault(card => card.Id == id);
    }

    public IEnumerable<Card> ReadAllCards()
    {
        return _cards;
    }

    public IEnumerable<Card> ReadCardsOfType(CardType type)
    {
        return _cards.Where(card => card != null && card.Type == type).ToList();
    }

    public IEnumerable<Card> ReadCardsFromDeck(long deckId)
    {
        throw new NotImplementedException();
    }

    public Card ReadCardWithDecks(int id)
    {
        throw new NotImplementedException();
    }

    public Card ReadCardWithSets(int id)
    {
        throw new NotImplementedException();
    }

    public Card ReadCardWithDecksAndSets(int id)
    {
        throw new NotImplementedException();
    }

    public void CreateCard(Card card)
    {
        card.Id = _cardId++;
        _cards.Add(card);
    }

    public Deck ReadDeck(int id)
    {
        return _decks.FirstOrDefault(deck => deck.Id == id);
    }

    public IEnumerable<Deck> ReadAllDecks()
    {
        return _decks;
    }

    public List<Deck> ReadDeckByNameAndCreationDate(Deck deck)
    {
        return _decks.Where(d => d.Name == deck.Name && d.CreationDate == deck.CreationDate).ToList();
    }

    public IEnumerable<Deck> ReadDeckByNameAndCreationDate(string name, DateTime creationDate)
    {
        return _decks.Where(card => name != null && (card.Name.Contains(name) || card.CreationDate.ToShortDateString() == creationDate.ToShortDateString())).ToList();
    }

    public IEnumerable<Deck> ReadAllDecksWithCards()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Deck> ReadCardsOfDeck(long deckId)
    {
        throw new NotImplementedException();
    }

    public DeckEntry ReadDeckEntryWithCard(long id)
    {
        throw new NotImplementedException();
    }

    public void CreateDeck(Deck deck)
    {
        deck.Id = _deckId++;
        _decks.Add(deck);
    }

    public void CreateDeckEntry(DeckEntry deckEntry)
    {
        _deckEntries.Add(deckEntry);
    }

    public void DeleteDeckEntry(long cardId, long deckId)
    {
        throw new NotImplementedException();
    }

    public Set ReadSet(int id)
    {
        return _sets.FirstOrDefault(set => set.Id == id);
    }

    public IEnumerable<Set> ReadAllSets()
    {
        return _sets;
    }

    public IEnumerable<Set> ReadAllSetsWithCard()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Set> ReadCardsOfSet(long setId)
    {
        throw new NotImplementedException();
    }

    public void CreateSet(Set set)
    {
        _sets.Add(set);
    }

    public void CreateSetEntry(SetEntry setEntry)
    {
        _setEntries.Add(setEntry);
    }

    public void DeleteSetEntry(long cardId, long setId)
    {
        throw new NotImplementedException();
    }

    public List<DeckEntry> ReadDeckEntriesOfDeck(long deckId)
    {
        throw new NotImplementedException();
    }

    public DeckEntry ReadDeckEntry(long id)
    {
        throw new NotImplementedException();
    }

    public static void Seed()
    {
        _cards.Add( new Card("Elderwoorth Scion",
            CardType.Creature,
            CardAbility.Trample,
            CardColour.Green,
            6,
            0.12,
            "Spell you cast that target Elderwoorth Scion cost 2 less to cast. Spells your opponents cast that target Elderwoorth Scion cost 2 more to cast.",
            true));
        _cards.Add( new Card("Windstorm Drake",
            CardType.Creature,
            CardAbility.Flying,
            CardColour.Blue,
            5,
            0.08,
            "Other creatures you control with flying get +1/+0.",
            false));
        _cards.Add( new Card("Unbreakable Formation",
            CardType.Instant,
            null!,
            CardColour.White,
            3,
            0.13,
            "Creatures you control gain indestructible until end of turn. Addendum — If you cast this spell during your main phase, put a +1/+1 counter on each of those creatures and they gain vigilance until end of turn.",
            false));
        _cards.Add( new Card("Sol Ring",
            CardType.Artifact,
            null!,
            CardColour.Colourless,
            1,
            0.35,
            "Tap: Add CC.",
            true));

        _sets.Add(new Set(
            "Commander2018",
            "C18",
            new DateTime(2018, 08, 10)));
        _sets.Add(new Set(
            "Jumpstart ", 
            "JMP", 
            new DateTime(2022, 12, 02)));
        _sets.Add(new Set(
            "Ravnica Allegiance", 
            "RNA", 
            new DateTime(2019, 01, 25)));
        _sets.Add(new Set(
            "Commander Masters", 
            "CMM", 
            new DateTime(2023, 08, 04)));
        
        _decks.Add(new Deck(
            "Green-White Deck", 
                DateTime.Now, 
                "This is a green-white deck with powerful creatures."));

        _decks.Add( new Deck(
            "Blue Deck",
            DateTime.Now, 
            "A deck focused on flying creatures and artifacts."));

        _decks.Add( new Deck(
            "Red-Black Deck", 
            DateTime.Now, 
            "A deck with a mix of instant spells and artifacts."));

        _decks.Add( new Deck(
            "White Deck", 
            DateTime.Now, 
            "A deck with flying creatures and an instant spell."));
        
        _deckEntries.Add(new DeckEntry(_cards[0], _decks[0], 3, DateTime.Now));
        _deckEntries.Add(new DeckEntry(_cards[1], _decks[0], 3, DateTime.Now));

        _deckEntries.Add(new DeckEntry(_cards[2], _decks[1], 3, DateTime.Now));
        _deckEntries.Add(new DeckEntry(_cards[3], _decks[1], 3, DateTime.Now));

        _deckEntries.Add(new DeckEntry(_cards[0], _decks[2], 3, DateTime.Now));
        _deckEntries.Add(new DeckEntry(_cards[2], _decks[2], 3, DateTime.Now));

        _deckEntries.Add(new DeckEntry(_cards[1], _decks[3], 3, DateTime.Now));
        _deckEntries.Add(new DeckEntry(_cards[3], _decks[3], 3, DateTime.Now));
        
        _setEntries.Add(new SetEntry(_cards[0], _sets[0], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[1], _sets[0], DateTime.Now));

        _setEntries.Add(new SetEntry(_cards[2], _sets[1], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[3], _sets[1], DateTime.Now));

        _setEntries.Add(new SetEntry(_cards[0], _sets[2], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[2], _sets[2], DateTime.Now));

        _setEntries.Add(new SetEntry(_cards[1], _sets[3], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[3], _sets[3], DateTime.Now));
    }
}