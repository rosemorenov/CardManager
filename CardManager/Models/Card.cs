using System.Collections.Generic;

namespace CardManager.Models
{
    public class Card
    {
        public int CardId { get; set; }

        public string Name { get; set; } = "";
        public string CardSet { get; set; } = "";
        public string Rarity { get; set; } = "";
        public string? ManaCost { get; set; } = "";
        public string? CardType { get; set; } = "";

        public string? SubType { get; set; }
        public int? Power { get; set; }
        public decimal? Value { get; set; }
        public int? Quantity { get; set; }

        public string ImageFileName { get; set; } = "";

        public List<DeckCard> DeckCards { get; set; } = new();
    }
}