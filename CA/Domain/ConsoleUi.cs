namespace MagicTheGatheringManagement.Domain;

public class ConsoleUi
{
    private readonly List<Card> _cards;
    private readonly List<Set> _sets;
    private readonly List<Deck> _decks;
    
    public ConsoleUi()
    {
        _cards = new List<Card>();
        _sets = new List<Set>();
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

            string choice = Console.ReadLine();

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
        Card dovinArchitectOfLaw = new Card(
            "Dovin, Architect of Law",
            CardType.Planeswalker,
            6,
            1.84,
            "",
            new DateTime(2019, 01, 25),
            true);

        _cards.Add(dovinArchitectOfLaw);
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