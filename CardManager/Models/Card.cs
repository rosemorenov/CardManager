using System.Collections.Generic;

namespace CardManager.Models
{
    public class Card
    {
        // Primary key for the Cards table
        public int CardId { get; set; }

        // Display name of the card (e.g., "Fireball")
        public string Name { get; set; } = "";

        // The set or expansion the card belongs to (e.g., "TLA", "TLE")
        public string CardSet { get; set; } = "";

        // Rarity classification (e.g., Common, Rare, Mythic)
        public string Rarity { get; set; } = "";

        // Optional mana cost value (e.g., "{3}{R}")
        public string? ManaCost { get; set; } = "";

        // The primary card type (e.g., Creature, Spell, Artifact)
        public string? CardType { get; set; } = "";

        // The subtype of the card (e.g., "Goblin", "Warrior"), if applicable
        public string? SubType { get; set; }

        // Power value for creature cards — nullable for non-creature cards
        public int? Power { get; set; }

        // Monetary or collection value of the card
        public decimal? Value { get; set; }

        // Total number of copies owned in the collection
        public int? Quantity { get; set; }

        // File name of the associated image stored in /wwwroot/images
        public string ImageFileName { get; set; } = "";

        // Navigation property for the many-to-many relationship between Cards and Decks
        // A card can belong to many DeckCards entries
        public List<DeckCard> DeckCards { get; set; } = new();
    }
}