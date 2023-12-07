using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using MagicTheGatheringManagement.Domain;
using MagicTheGatheringManagement.Extensions;
using MTGM.BL;
using MTGM.BL.Domain;

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
        var quit = false;

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
            Console.WriteLine("6) Add card");
            Console.WriteLine("7) Add deck");
            Console.WriteLine("8) Add set");
            Console.WriteLine("9) Add card to deck");
            Console.WriteLine("10) Add card to set");
            Console.WriteLine("11) Remove card from deck");
            Console.WriteLine("12) Remove card from set");
            Console.Write("Choice (0-6): ");

            var choice = Console.ReadLine();

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
                case "6":
                    AddCard();
                    break;
                case "7":
                    AddDeck();
                    break;
                case "8":
                    AddSet();
                    break;
                case "9":
                    AddCardToDeck();
                    break;
                case "10":
                    AddCardToSet();
                    break;
                case "11":
                    RemoveCardFromSet();
                    break;
                case "12":
                    RemoveCardFromDeck();
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
        Console.WriteLine("All Sets");
        Console.WriteLine("========");
        foreach (var set in _manager.GetAllSetsWithCard())
        {
            Console.WriteLine(set);
        }
    }

    private void ShowAllDecks()
    {
        Console.WriteLine("All Decks");
        Console.WriteLine("=========");
        foreach (var deck in _manager.GetAllDecksWithCards())
        {
            if (deck != null)
            {
                Console.WriteLine(deck.GetString());
            }
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
    
   private void AddCard()
    {
        var cardColours = new CardColour();

        Console.WriteLine("Name: ");
        var name = Console.ReadLine();

        // Enum values and a dictionary to map numbers to enum values
        var cardTypes = Enum.GetValues(typeof(CardType));
        var cardTypeMap = new Dictionary<int, CardType>();
        for (int i = 0; i < cardTypes.Length; i++)
        {
            cardTypeMap[i + 1] = (CardType)cardTypes.GetValue(i)!;
        }
        var cardAbilty = new CardAbility();
        var cardAbilities = Enum.GetValues(typeof(CardAbility));
        var cardAbiltyMap = new Dictionary<int, CardAbility>();
        for (int i = 0; i < cardAbilities.Length; i++)
        {
            cardAbiltyMap[i + 1] = (CardAbility)cardAbilities.GetValue(i)!;
        }

        Console.WriteLine("Choose a Card Type:");
        foreach (var entry in cardTypeMap)
        {
            Console.WriteLine($"{entry.Key}) {entry.Value}");
        }
        if (int.TryParse(Console.ReadLine(), out var typeChoice) && cardTypeMap.ContainsKey(typeChoice))
        {
            var type = cardTypeMap[typeChoice];
            
            var chosenAbility = 0;
            Console.WriteLine("Choose a Card Ability (0=None):");
            foreach (var entry in cardAbiltyMap)
            {
                Console.WriteLine($"{entry.Key}) {entry.Value}");
            }
            if (int.TryParse(Console.ReadLine(), out chosenAbility) && cardAbiltyMap.ContainsKey(chosenAbility))
            {
                cardAbilty = (CardAbility)chosenAbility;
            }
            else if (chosenAbility != 0)
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
            

            var cardColour = 0;
            Console.WriteLine("Card Colours (1=White, 2=Blue, 3=Black, ...): ");
            if (int.TryParse(Console.ReadLine(), out cardColour) && Enum.IsDefined(typeof(CardColour), cardColour))
            {
                cardColours =(CardColour)cardColour;
            }
            else if (cardColour != 0)
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine("Mana Cost: ");
            if (!int.TryParse(Console.ReadLine(), out var manaCost))
            {
                Console.WriteLine("Invalid input for Mana Cost. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Price: ");
            if (!double.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid input for Price. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Description: ");
            var description = Console.ReadLine();

            Console.WriteLine("Is Foil (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out var isFoil))
            {
                Console.WriteLine("Invalid input for Is Foil. Please enter 'true' or 'false'.");
                return;
            }
            
            _manager.AddCard(name, type, cardAbilty, cardColours, manaCost, price, description, isFoil);
            
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }


    
   private void AddDeck()
   {
       Console.WriteLine("Deck Name: ");
       var deckName = Console.ReadLine();

       Console.WriteLine("Select Cards for the Deck (Enter card numbers, 0 to finish):");

       // Fetch all available cards from the _manager
       var availableCards = _manager.GetAllCards();

       var enumerable = availableCards.ToList();
       for (int i = 0; i < enumerable.Count(); i++)
       {
           Console.WriteLine($"{i + 1}) {enumerable[i].Name} - {enumerable[i].Type}");
       }

       var selectedCards = new List<Card>();

       while (true)
       {
           if (int.TryParse(Console.ReadLine(), out int cardChoice))
           {
               if (cardChoice == 0)
               {
                   break;
               }
               else if (cardChoice >= 1 && cardChoice <= enumerable.Count)
               {
                   // Add the selected card to the list of selected cards
                   selectedCards.Add(enumerable[cardChoice - 1]);
                   Console.WriteLine($"Added {enumerable[cardChoice - 1].Name} to the deck.");
               }
               else
               {
                   Console.WriteLine("Invalid choice. Please select a valid card or enter 0 to finish.");
               }
           }
           else
           {
               Console.WriteLine("Invalid input. Please enter a number.");
           }
       }

       var creationDate = DateTime.Now;

       Console.WriteLine("Notes: ");
       var notes = Console.ReadLine();

       // Create a deck with the selected cards and add it to the _manager
       _manager.AddDeck(deckName, selectedCards, creationDate, notes);

       Console.WriteLine("Deck created and added to the manager successfully!");
   }
   
   private void AddSet()
   {
         Console.WriteLine("Set Name: ");
         var setName = Console.ReadLine();
    
         Console.WriteLine("Set Code: ");
         var setCode = Console.ReadLine();
    
         Console.WriteLine("Release Date: ");
         var releaseDate = Console.ReadLine();
    
         _manager.AddSet(setName, setCode, Convert.ToDateTime(releaseDate));
    
         Console.WriteLine("Set created and added to the manager successfully!");
   }

   private void AddCardToDeck()
   {
       Console.WriteLine("Select Deck to add card to (Enter deck number):");
       var decks = _manager.GetAllDecks();
       foreach (Deck deck in decks)
       {
           Console.WriteLine($"[{deck.Id}] {deck.Name}");
       }

       int deckChoice;
       while (true)
       {
           if (int.TryParse(Console.ReadLine(), out deckChoice))
           {
               if (decks.Any(deck => deck.Id == deckChoice))
               {
                   break;
               }
           }
           Console.WriteLine("Invalid input. Please enter a valid deck number:");
       }

       Console.WriteLine("Select Card to add to deck (Enter card number):");
       var cards = _manager.GetAllCards();
       foreach (Card card in cards)
       {
           Console.WriteLine($"[{card.Id}] {card.Name}");
       }

       int cardChoice;
       while (true)
       {
           if (int.TryParse(Console.ReadLine(), out cardChoice))
           {
               if (cards.Any(card => card.Id == cardChoice))
               {
                   break;
               }
           }
           Console.WriteLine("Invalid input. Please enter a valid card number:");
       }

       Console.WriteLine("Select amount of cards to add to deck (Enter amount):");
       int amountChoice;
       while (true)
       {
           if (int.TryParse(Console.ReadLine(), out amountChoice) && amountChoice > 0)
           {
               break;
           }
           Console.WriteLine("Invalid input. Please enter a valid positive number:");
       }

       _manager.AddDeckEntry(
           _manager.GetDeck(deckChoice),
           _manager.GetCard(cardChoice),
           amountChoice,
           DateTime.Now
       );
   }

   private void AddCardToSet()
   {
         Console.WriteLine("Select Set to add card to (Enter set number):");
         var sets = _manager.GetAllSets();
         foreach (Set set in sets)
         {
              Console.WriteLine($"[{set.Id}] {set.Name}");
         }
    
         int setChoice;
         while (true)
         {
              if (int.TryParse(Console.ReadLine(), out setChoice))
              {
                if (sets.Any(set => set.Id == setChoice))
                {
                     break;
                }
              }
              Console.WriteLine("Invalid input. Please enter a valid set number:");
         }
    
         Console.WriteLine("Select Card to add to set (Enter card number):");
         var cards = _manager.GetAllCards();
         foreach (Card card in cards)
         {
              Console.WriteLine($"[{card.Id}] {card.Name}");
         }
    
         int cardChoice;
         while (true)
         {
              if (int.TryParse(Console.ReadLine(), out cardChoice))
              {
                if (cards.Any(card => card.Id == cardChoice))
                {
                     break;
                }
              }
              Console.WriteLine("Invalid input. Please enter a valid card number:");
         }
    
         _manager.AddSetEntry(
              _manager.GetCard(cardChoice),
              _manager.GetSet(setChoice),
              DateTime.Now
         );
   }

   private void RemoveCardFromSet()
   {
        Console.WriteLine("Select Set to remove card from (Enter set number):");
        var sets = _manager.GetAllSets();
        foreach (Set set in sets)
        {
            Console.WriteLine($"[{set.Id}] {set.Name}");
        }
        
        int setChoice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out setChoice))
            {
                if (sets.Any(set => set.Id == setChoice))
                {
                    break;
                }
            }
            Console.WriteLine("Invalid input. Please enter a valid set number:");
        }
        
        Console.WriteLine("Select Card to remove from set (Enter card number):");
        var cards = _manager.GetAllCards();
        int cardChoice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out cardChoice))
            {
                if (cards.Any(card => card.Id == cardChoice))
                {
                    break;
                }
            }
            Console.WriteLine("Invalid input. Please enter a valid card number:");
        }
        
        _manager.RemoveSetEntry(cardChoice, setChoice);
   }

   private void RemoveCardFromDeck()
   {
        Console.WriteLine("Select Deck to remove card from (Enter deck number):");
        var decks = _manager.GetAllDecks();
        foreach (Deck deck in decks)
        {
            Console.WriteLine($"[{deck.Id}] {deck.Name}");
        }
        
        int deckChoice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out deckChoice))
            {
                if (decks.Any(deck => deck.Id == deckChoice))
                {
                    break;
                }
            }
            Console.WriteLine("Invalid input. Please enter a valid deck number:");
        }
        
        Console.WriteLine("Select Card to remove from deck (Enter card number):");
        var cards = _manager.GetAllCards();
        int cardChoice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out cardChoice))
            {
                if (cards.Any(card => card.Id == cardChoice))
                {
                    break;
                }
            }
            Console.WriteLine("Invalid input. Please enter a valid card number:");
        }
       
        
        _manager.RemoveDeckEntry(cardChoice, deckChoice);
   }



}