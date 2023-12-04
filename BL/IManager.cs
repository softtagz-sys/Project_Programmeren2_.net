﻿using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;

namespace MTGM.BL;

public interface IManager
{
    public Card GetCard(int id);
    public IEnumerable<Card> GetAllCards();
    public IEnumerable<Card> GetCardsOfType(CardType type);
    public Card AddCard(string name, CardType type, List<CardAbility> cardAbilities, List<CardColour> cardColours, int manaCost, double price, string description, bool isFoil);
    
    public Deck GetDeck(int id);
    public IEnumerable<Deck> GetAllDecks();
    public IEnumerable<Deck> GetDeckByNameAndCreationDate(String name, DateTime creationDate);
    public Deck AddDeck(string name, List<Card> cards, DateTime creationDate, string notes);
    public DeckEntry AddDeckEntry(Deck deck, Card card, int amount, DateTime creationDate);
    public IEnumerable<DeckEntry> GetAllDeckEntries();
    public Set AddSet(String name, String code, DateTime releaseDate);
    public SetEntry AddSetEntry(Card card, Set set, DateTime addedOn);
}