using System;
using System.Collections.Generic;
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
            Console.WriteLine("5) Show decks created after a specific date");
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
                    ShowDecksCreatedAfterDate();
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
        
        //TODO: Add decks
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
        Console.Write("Enter card type (Creature, Spell, Artifact, etc.): ");
        var cardTypeInput = Console.ReadLine();
        if (Enum.TryParse(cardTypeInput, out CardType cardType))
        {
            var matchingCards = _cards.Where(card => card.Type == cardType).ToList();
            if (matchingCards.Any())
            {
                Console.WriteLine($"Cards of type {cardType}");
                Console.WriteLine("=================");
                foreach (var card in matchingCards)
                {
                    Console.WriteLine(card);
                }
            }
            else
            {
                Console.WriteLine("No cards of the specified type found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid card type.");
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

    private void ShowDecksCreatedAfterDate()
    {
        Console.Write("Enter a date (yyyy/mm/dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime creationDate))
        {
            var matchingDecks = _decks.Where(deck => deck.CreationDate > creationDate).ToList();
            if (matchingDecks.Any())
            {
                Console.WriteLine("Decks created after the specified date");
                Console.WriteLine("=====================================");
                foreach (var deck in matchingDecks)
                {
                    Console.WriteLine(deck);
                }
            }
            else
            {
                Console.WriteLine("No decks created after the specified date found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid date format.");
        }
    }
}