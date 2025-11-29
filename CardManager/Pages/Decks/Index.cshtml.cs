using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class IndexModel : PageModel
    {
        // Entity Framework database context
        // Provides access to the Decks, Cards, and DeckCards tables.
        private readonly CardCollectionContext _context;

        // Constructor receives the database context from dependency injection
        public IndexModel(CardCollectionContext context)
        {
            _context = context;
        }

        // List to store all decks retrieved from the database
        // This list is used by the Razor page for display
        public List<Deck> Decks { get; set; } = new();

        // GET request handler:
        // Loads all decks from the database along with their related DeckCards and Card objects.
        public async Task OnGetAsync()
        {
            // Load all decks and include related records:
            // 1. Include(d => d.DeckCards) loads the join table entries.
            // 2. ThenInclude(dc => dc.Card) loads each associated card.
            Decks = await _context.Decks
                .Include(d => d.DeckCards)       // Load deck-card relationships
                .ThenInclude(dc => dc.Card)      // Load actual card data for each relationship
                .ToListAsync();                  // Execute query asynchronously
        }
    }
}
