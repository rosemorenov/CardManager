namespace CardManager.Models
{
    public class DeckCard
    {
        // Primary key for the DeckCards join table.
        // Each record represents a single card entry inside a specific deck.
        public int DeckCardId { get; set; }

        // Foreign key linking this entry to a specific Deck.
        // A deck can contain many DeckCard entries.
        public int DeckId { get; set; }

        // Foreign key linking this entry to a specific Card.
        // A card can appear in many decks.
        public int CardId { get; set; }

        // Navigation property for the Deck this join entry belongs to.
        // Marks the "many" side of the Deck → DeckCard relationship.
        public Deck Deck { get; set; } = null!;

        // Navigation property for the Card included in this deck.
        // Marks the "many" side of the Card → DeckCard relationship.
        public Card Card { get; set; } = null!;
    }
}