using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MagicTheGatheringManagement.Domain;

public class ConsoleUi
{
    private readonly List<Set> _sets;
    private readonly List<Card> _cards;
    private readonly List<Deck> _decks;
    
    public ConsoleUi()
    {
        _sets = new List<Set>();
        _cards = new List<Card>();
        _decks = new List<Deck>();
    }

    public void Run()
    {
        Seed();

        bool quit = false;

        while (!quit)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all cards");
            Console.WriteLine("2) Show cards of a specific type");
            Console.WriteLine("3) Show all sets");
            Console.WriteLine("4) Show all decks");
            Console.WriteLine("5) Show decks with name and/or creation date");
            Console.Write("Choice (0-5): ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    quit = true;
                    break;
                case "1":
                    ShowAllCards();
                    break;
                case "2":
                    ShowCardsOfType();
                    break;
                case "3":
                    ShowAllSets();
                    break;
                case "4":
                    ShowAllDecks();
                    break;
                case "5":
                    ShowDecksByNameAndOrDate();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private void Seed()
    {
        _cards.Add(new Card(
            "Elderwoorth Scion",
            CardType.Creature,
            new List<CardAbility> {CardAbility.Trample, CardAbility.Lifelink },
            new List<CardColour> {CardColour.Green, CardColour.White },
            6,
            0.12,
            "Spell you cast that target Elderwoorth Scion cost 2 less to cast. Spells your opponents cast that target Elderwoorth Scion cost 2 more to cast.",
            true));
        _cards.Add(new Card(
            "Windstorm Drake",
            CardType.Creature,
            new List<CardAbility>{CardAbility.Flying},
            new List<CardColour> { CardColour.Blue },
            5,
            0.08,
            "Other creatures you control with flying get +1/+0.",
            false));
        _cards.Add(new Card(
            "Unbreakable Formation",
            CardType.Instant,
            null!,
            new List<CardColour> { CardColour.White },
            3,
            0.13,
            "Creatures you control gain indestructible until end of turn. Addendum — If you cast this spell during your main phase, put a +1/+1 counter on each of those creatures and they gain vigilance until end of turn.",
            false));
        _cards.Add(new Card(
            "Sol Ring",
            CardType.Artifact,
            null!,
            new List<CardColour> { CardColour.Colourless },
            1,
            0.35,
            "Tap: Add CC.",
            true));

        _sets.Add(new Set("Commander2018", "C18", new DateTime(2018, 08, 10)));
        _sets.Add(new Set("Jumpstart ", "JMP", new DateTime(2022, 12, 02)));
        _sets.Add(new Set("Ravnica Allegiance", "RNA", new DateTime(2019, 01, 25)));
        _sets.Add(new Set("Commander Masters", "CMM", new DateTime(2023, 08, 04)));
        
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
    
    private void ShowAllCards()
    {
        Console.WriteLine("All Cards");
        Console.WriteLine("=========");
        foreach (var card in _cards)
        {
            Console.WriteLine(card);
        }
    }
    
    private void ShowCardsOfType()
    {
        Console.WriteLine("Card Types");
        Console.WriteLine("==========");
        foreach (var cardType in Enum.GetValues<CardType>())
        {
            Console.WriteLine($"{(byte)cardType}) {cardType}");
        }
        Console.Write("Choice (1-7): ");
        if (byte.TryParse(Console.ReadLine(), out byte choice))
        {
            if (Enum.IsDefined(typeof(CardType), choice))
            {
                ShowCardsOfType((CardType)choice);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }

    private void ShowCardsOfType(CardType cardType)
    {
        if (!Enum.IsDefined(typeof(CardType), cardType))
            throw new InvalidEnumArgumentException(nameof(cardType), (int)cardType, typeof(CardType));
        foreach (var card in _cards.Where(card => card.Type == cardType))
        {
            Console.WriteLine(card);
        }
    }

    private void ShowAllSets()
    {
        Console.WriteLine("All Sets");
        Console.WriteLine("========");
        foreach (var set in _sets)
        {
            Console.WriteLine(set);
        }
    }

    private void ShowAllDecks()
    {
        Console.WriteLine("All Decks");
        Console.WriteLine("=========");
        foreach (var deck in _decks)
        {
            Console.WriteLine(deck);
        }
    }

    private void ShowDecksByNameAndOrDate()
    {
        Console.WriteLine("Enter (part of) a name or leave blank");
        string? name = Console.ReadLine();
        
        Console.WriteLine("Enter a full creation date (yyyy/mm/dd) or leave blank");
        string? date = Console.ReadLine();

        foreach (var deck in _decks.Where(card => name != null && (card.Name.Contains(name) || card.CreationDate.ToShortDateString() == date)))
        {
            Console.WriteLine(deck);
        }
    }
}