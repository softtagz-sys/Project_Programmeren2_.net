using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.DAL;

public class InMemoryRepository : IRepository
{
    private static List<Set> _sets;
    private static List<Card> _cards;
    private static List<Deck> _decks;

    private int _cardId = 1;
    private int _deckId = 1;
    
    public InMemoryRepository()
    {
        _sets = new List<Set>();
        _cards = new List<Card>();
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

    public List<Deck> ReadDeckByNameAndCreationDate(Card card)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Deck> ReadDeckByNameAndCreationDate(string name, DateTime creationDate)
    {
        return _decks.Where(card => name != null && (card.Name.Contains(name) || card.CreationDate.ToShortDateString() == creationDate.ToShortDateString())).ToList();
    }

    public void CreateDeck(Deck deck)
    {
        deck.Id = _deckId++;
        _decks.Add(deck);
    }
    
    public static void Seed()
    {
        _cards.Add( new Card("Elderwoorth Scion",
            CardType.Creature,
            new List<CardAbility> {CardAbility.Trample, CardAbility.Lifelink },
            new List<CardColour> {CardColour.Green, CardColour.White },
            6,
            0.12,
            "Spell you cast that target Elderwoorth Scion cost 2 less to cast. Spells your opponents cast that target Elderwoorth Scion cost 2 more to cast.",
            true));
        _cards.Add( new Card("Windstorm Drake",
            CardType.Creature,
            new List<CardAbility>{CardAbility.Flying},
            new List<CardColour> { CardColour.Blue },
            5,
            0.08,
            "Other creatures you control with flying get +1/+0.",
            false));
        _cards.Add( new Card("Unbreakable Formation",
            CardType.Instant,
            null!,
            new List<CardColour> { CardColour.White },
            3,
            0.13,
            "Creatures you control gain indestructible until end of turn. Addendum — If you cast this spell during your main phase, put a +1/+1 counter on each of those creatures and they gain vigilance until end of turn.",
            false));
        _cards.Add( new Card("Sol Ring",
            CardType.Artifact,
            null!,
            new List<CardColour> { CardColour.Colourless },
            1,
            0.35,
            "Tap: Add CC.",
            true));

        _sets.Add(new Set(
            1,
            "Commander2018",
            "C18",
            new DateTime(2018, 08, 10),
            new List<Card>
            {
                _cards[0], // Elderwoorth Scion
                _cards[3]  // Sol Ring
            }));
        _sets.Add(new Set(
            2, 
            "Jumpstart ", 
            "JMP", 
            new DateTime(2022, 12, 02),
            new List<Card>
            {
                _cards[1], // Windstorm Drake
                _cards[2]  // Unbreakable Formation
            }));
        _sets.Add(new Set(
            3, 
            "Ravnica Allegiance", 
            "RNA", 
            new DateTime(2019, 01, 25),
            new List<Card>
            {
                _cards[1], // Windstorm Drake
                _cards[3]  // Sol Ring
            }));
        _sets.Add(new Set(
            4, 
            "Commander Masters", 
            "CMM", 
            new DateTime(2023, 08, 04),
            new List<Card>
            {
                _cards[0], // Elderwoorth Scion
                _cards[2]  // Unbreakable Formation
            }));
        
        _decks.Add( new Deck("Green-White Deck", new List<Card>
        {
            _cards[0],  // Elderwoorth Scion
            _cards[2]   // Unbreakable Formation
        }, DateTime.Now, "This is a green-white deck with powerful creatures."));

        _decks.Add( new Deck("Blue Deck", new List<Card>
        {
            _cards[1],  // Windstorm Drake
            _cards[3]   // Sol Ring
        }, DateTime.Now, "A deck focused on flying creatures and artifacts."));

        _decks.Add( new Deck("Red-Black Deck", new List<Card>
        {
            _cards[2],  // Unbreakable Formation
            _cards[3]   // Sol Ring
        }, DateTime.Now, "A deck with a mix of instant spells and artifacts."));

        _decks.Add( new Deck("White Deck", new List<Card>
        {
            _cards[2],  // Unbreakable Formation
            _cards[1]   // Windstorm Drake
        }, DateTime.Now, "A deck with flying creatures and an instant spell."));
    }
}