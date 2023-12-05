using System.Collections;
using MagicTheGatheringManagement.Domain;
using Microsoft.EntityFrameworkCore;
using MTGM.BL.Domain;

namespace MTGM.DAL.EF;

public static class DataSeeder
{
    private static readonly IList<Set> _sets;
    private static readonly IList<SetEntry> _setEntries;
    private static readonly IList<Card> _cards;
    private static readonly IList<DeckEntry> _deckEntries;
    private static readonly IList<Deck> _decks;
    
    static DataSeeder()
    {
        _sets = new List<Set>();
        _setEntries = new List<SetEntry>();
        _cards = new List<Card>();
        _deckEntries = new List<DeckEntry>();
        _decks = new List<Deck>();
    }
    
    public static void Seed(MtgmDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        
        
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
;

        _setEntries.Add(new SetEntry(_cards[0], _sets[2], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[2], _sets[2], DateTime.Now));
;

        _setEntries.Add(new SetEntry(_cards[1], _sets[3], DateTime.Now));
        _setEntries.Add(new SetEntry(_cards[3], _sets[3], DateTime.Now));

        foreach (Card card in _cards)
        {
            context.Cards.Add(card);
        }

        foreach (Set set in _sets)
        {
            context.Sets.Add(set);
        }

        foreach (Deck deck in _decks)
        {
            context.Decks.Add(deck);
        }

        foreach (DeckEntry deckEntry in _deckEntries)
        {
            context.DeckEntries.Add(deckEntry);
        }
        
        foreach (SetEntry setEntry in _setEntries)
        {
            context.SetEntries.Add(setEntry);
        }
        
        context.SaveChanges();
        
        context.ChangeTracker.Clear();
    }
}