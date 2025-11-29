using Microsoft.EntityFrameworkCore;
using CardManager.Models;

namespace CardManager.Data
{
    public class CardCollectionContext : DbContext
    {
        public CardCollectionContext(DbContextOptions<CardCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; } = default!;
        public DbSet<Deck> Decks { get; set; } = default!;
        public DbSet<DeckCard> DeckCards { get; set; } = default!;
    }
}