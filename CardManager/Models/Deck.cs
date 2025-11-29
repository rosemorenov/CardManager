namespace CardManager.Models
{
    public class Deck
    {
        // Primary key for the Decks table
        public int DeckId { get; set; }

        // Name of the deck (e.g., "Fire Tribe Aggro", "Blue Control")
        public string DeckName { get; set; } = "";

        // Navigation property representing the many-to-many relationship
        // A deck can contain many cards, each represented through a DeckCard join entry.
        public List<DeckCard> DeckCards { get; set; } = new();
    }
}