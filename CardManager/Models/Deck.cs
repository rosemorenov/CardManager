using System.Collections.Generic;

namespace CardManager.Models
{
    public class Deck
    {
        public int DeckId { get; set; }
        public string DeckName { get; set; } = "";

        public List<DeckCard> DeckCards { get; set; } = new();
    }
}