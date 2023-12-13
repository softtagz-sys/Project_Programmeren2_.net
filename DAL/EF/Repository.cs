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

        public Card ReadCardWithDecks(int id)
        {
            return _context.Cards
                .Include(c => c.DeckEntries)
                .ThenInclude(de => de.Deck)
                .Single(c => c.Id == id);
        }

        public Card ReadCardWithSets(int id)
        {
            return _context.Cards
                .Include(c => c.SetEntries)
                .ThenInclude(se => se.Set)
                .Single(c => c.Id == id);
        }

        public Card ReadCardWithDecksAndSets(int id)
        {
            return _context.Cards
                .Include(c => c.DeckEntries)
                .ThenInclude(de => de.Deck)
                .Include(c => c.SetEntries)
                .ThenInclude(se => se.Set)
                .Single(c => c.Id == id);
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

        public IEnumerable<Deck> ReadAllDecksWithCards()
        {
            return _context.Decks
                .Include(d => d.Cards)
                .ThenInclude(c => c.Card)
                .ToList();
        }

        public IEnumerable<Deck> ReadCardsOfDeck(long deckId)
        {
            return _context.Decks
                .Include(d => d.Cards)
                .ThenInclude(c => c.Card)
                .Where(d => d.Id == deckId)
                .ToList();
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

        public void DeleteDeckEntry(long cardId, long deckId)
        {
            var deckEntry = _context.DeckEntries.FirstOrDefault(de => de.Card.Id == cardId && de.Deck.Id == deckId);
            if (deckEntry != null) _context.DeckEntries.Remove(deckEntry);
            _context.SaveChanges();
        }

        public Set ReadSet(int id)
        {
            return _context.Sets.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Set> ReadAllSets()
        {
            return _context.Sets.ToList();
        }

        public IEnumerable<Set> ReadAllSetsWithCard()
        {
            return _context.Sets
                .Include(c => c.Cards)
                .ThenInclude(c => c.Card)
                .ToList();
        }

        public IEnumerable<Set> ReadCardsOfSet(long setId)
        {
            return _context.Sets
                .Include(c => c.Cards)
                .ThenInclude(c => c.Card)
                .Where(s => s.Id == setId)
                .ToList();
        }

        public void CreateSet(Set set)
        {
            _context.Sets.Add(set);
            _context.SaveChanges();
        }

        public void CreateSetEntry(SetEntry setEntry)
        {
            _context.SetEntries.Add(setEntry);
            _context.SaveChanges();
        }

        public void DeleteSetEntry(long cardId, long setId)
        {
            var setEntry = _context.SetEntries.FirstOrDefault(se => se.Card.Id == cardId && se.Set.Id == setId);
            if (setEntry != null) _context.SetEntries.Remove(setEntry);
            _context.SaveChanges();
        }
    }
}