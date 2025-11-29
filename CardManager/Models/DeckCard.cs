namespace CardManager.Models
{
    public class DeckCard
    {
        public int DeckCardId { get; set; }

        public int DeckId { get; set; }
        public int CardId { get; set; }

        public Deck Deck { get; set; } = null!;
        public Card Card { get; set; } = null!;
    }
}