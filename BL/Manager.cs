﻿using System.ComponentModel.DataAnnotations;
using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;
using MTGM.DAL;

namespace MTGM.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;

    public Manager(IRepository repository)
    {
        _repository = repository;
    }
    
    public Card GetCard(int id)
    {
        return _repository.ReadCard(id);
    }

    public IEnumerable<Card> GetAllCards()
    {
        return _repository.ReadAllCards();
    }

    public IEnumerable<Card> GetCardsOfType(CardType type)
    {
        return _repository.ReadCardsOfType(type);
    }

    public Card AddCard(string name, CardType type, List<CardAbility> cardAbilities, List<CardColour> cardColours, int manaCost, double price,
        string description, bool isFoil)
    {
        Card card = new Card(name, type, cardAbilities, cardColours, manaCost, price, description, isFoil);
        ValidateObject(card);
        _repository.CreateCard(card);
        return card;
    }

    public Deck GetDeck(int id)
    {
        return _repository.ReadDeck(id);
    }

    public IEnumerable<Deck> GetAllDecks()
    {
        return _repository.ReadAllDecks();
    }

    public IEnumerable<Deck> GetDeckByNameAndCreationDate(string name, DateTime creationDate)
    {
        return _repository.ReadDeckByNameAndCreationDate(name, creationDate);
    }

    public Deck AddDeck(string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Deck deck = new Deck(name, cards, creationDate, notes);
        ValidateObject(deck);
        _repository.CreateDeck(deck);
        return deck;
    }
    
    private void ValidateObject(Object o)
    {
        var validationContext = new ValidationContext(o);
        Validator.ValidateObject(o, validationContext, true);
    }
}