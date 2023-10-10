using System.ComponentModel;
using MagicTheGatheringManagement.Domain;
using MagicTheGatheringManagement.Extensions;
using MTGM.BL;


namespace MagicTheGatheringManagement;

public class ConsoleUi
{
    private readonly IManager _manager;
    
    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }
    
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
            Console.WriteLine("3) Show all decks");
            Console.WriteLine("4) Show decks with name and/or creation date");
            Console.Write("Choice (0-4): ");

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
                    ShowAllDecks();
                    break;
                case "4":
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
        foreach (var card in _manager.GetAllCards())
        {
            if (card != null)
            {
                Console.WriteLine(card.GetString());
            }
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
        foreach (var card in _manager.GetCardsOfType(cardType))
        {
            Console.WriteLine(card.GetString());
        }
    }

    private void ShowAllSets()
    {
        // Console.WriteLine("All Sets");
        // Console.WriteLine("========");
        // foreach (var set in _manager.GetAllSets())
        // {
        //     Console.WriteLine(set);
        // }
    }

    private void ShowAllDecks()
    {
        Console.WriteLine("All Decks");
        Console.WriteLine("=========");
        foreach (var deck in _manager.GetAllDecks())
        {
            Console.WriteLine(deck.GetString());
        }
    }

    private void ShowDecksByNameAndOrDate()
    {
        Console.Write("Enter (part of) a name or leave blank: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter a full creation date (yyyy/mm/dd) or leave blank: ");
        string date = Console.ReadLine();

        foreach (var deck in _manager.GetDeckByNameAndCreationDate(name, Convert.ToDateTime(date)))
        {
            Console.WriteLine(deck.GetString());
        }
    }
}