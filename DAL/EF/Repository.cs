using MagicTheGatheringManagement.Domain;
using MTGM.BL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MTGM.DAL.EF
{
    public class Repository : IRepository
    {
        private MtgmDbContext _context;

        public Repository(MtgmDbContext context)
        {
            _context = context;
        }

        public Card ReadCard(int id)
        {
            return _context.Cards.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Card> ReadAllCards()
        {
            return _context.Cards.ToList();
        }

        public IEnumerable<Card> ReadCardsOfType(CardType type)
        {
            return _context.Cards.Where(c => c.Type == type).ToList();
        }

        public void CreateCard(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public Deck ReadDeck(int id)
        {
            return _context.Decks.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Deck> ReadAllDecks()
        {
            return _context.Decks.ToList();
        }

        public IEnumerable<Deck> ReadDeckByNameAndCreationDate(string name, DateTime creationDate)
        {
            var query = _context.Decks.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name == name);
            }

            if (creationDate != DateTime.MinValue)
            {
                query = query.Where(d => d.CreationDate == creationDate);
            }

            return query.ToList();
        }

        public void CreateDeck(Deck deck)
        {
            _context.Decks.Add(deck);
            _context.SaveChanges();
        }

        public void CreateDeckEntry(DeckEntry deckEntry)
        {
            _context.DeckEntries.Add(deckEntry);
            _context.SaveChanges();
        }

        public IEnumerable<DeckEntry> ReadAllDeckEntries()
        {
            return _context.DeckEntries.ToList();
        }

        public void CreateSet(Set set)
        {
            _context.Sets.Add(set);
            _context.SaveChanges();
        }

        public IEnumerable<DeckEntry> ReadDeckEntries()
        {
            return _context.DeckEntries.ToList();
        }

        public void CreateSetEntry(SetEntry setEntry)
        {
            _context.SetEntries.Add(setEntry);
            _context.SaveChanges();
        }

        public IEnumerable<SetEntry> ReadAllSetEntries()
        {
            return _context.SetEntries.ToList();
        }

        public IEnumerable<SetEntry> ReadSetEntries()
        {
            return _context.SetEntries.ToList();
        }
    }
}