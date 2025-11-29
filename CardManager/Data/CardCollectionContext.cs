using Microsoft.EntityFrameworkCore;
using CardManager.Models;

namespace CardManager.Data
{
    // This class represents the Entity Framework Core database context.
    // It defines which tables (DbSets) exist in the database and provides
    // access to them for querying and saving data.
    public class CardCollectionContext : DbContext
    {
        // Constructor - receives configuration options from dependency injection.
        // These options include the database provider (SQLite) and connection string.
        public CardCollectionContext(DbContextOptions<CardCollectionContext> options)
            : base(options)
        {
        }

        // DbSet<Card> represents the "Cards" table in the database.
        // EF Core will map the Card model properties to columns in this table.
        public DbSet<Card> Cards { get; set; } = default!;

        // DbSet<Deck> represents the "Decks" table in the database.
        // Each Deck can contain multiple cards through the DeckCards join table.
        public DbSet<Deck> Decks { get; set; } = default!;

        // DbSet<DeckCard> represents the join table between Decks and Cards.
        // This table handles the many-to-many relationship.
        public DbSet<DeckCard> DeckCards { get; set; } = default!;
    }
}