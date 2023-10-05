using System.ComponentModel;
using MTGM.BL.Domain;


namespace MagicTheGatheringManagement;

public class ConsoleUi
{
    public void Run()
    {

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
        Console.Write("Enter (part of) a name or leave blank: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter a full creation date (yyyy/mm/dd) or leave blank: ");
        string date = Console.ReadLine();

        foreach (var deck in _decks.Where(card => name != null && (card.Name.Contains(name) || card.CreationDate.ToShortDateString() == date)))
        {
            Console.WriteLine(deck);
        }
    }
}