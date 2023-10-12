﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text;
using MagicTheGatheringManagement.Domain;

namespace MTGM.BL.Domain;

public class Deck : IValidatableObject
{
    public Deck(string name, List<Card> cards, DateTime creationDate, string notes)
    {
        Name = name;
        Cards = cards;
        CreationDate = creationDate;
        Notes = notes;
        Id = _deckId++;
    }
    private static int _deckId = 1;
    public int Id { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "Name must be at least 1 character long")]
    public string Name { get; set; }
    public ICollection<Card> Cards { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    public string Notes { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ICollection<ValidationResult> errors = new List<ValidationResult>();

        if (CreationDate > DateTime.Now)
        {
            errors.Add(new ValidationResult("Creation date cannot be in the future.", new[] {nameof(CreationDate)}));
        }

        return errors;
    }
}