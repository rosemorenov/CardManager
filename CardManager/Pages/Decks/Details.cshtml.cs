using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class DetailsModel : PageModel
    {
        // EF Core database context for accessing Decks, Cards, and DeckCards
        private readonly CardCollectionContext _context;

        // Constructor receives the database context through dependency injection
        public DetailsModel(CardCollectionContext context)
        {
            _context = context;
        }

        // The deck currently being displayed
        public Deck Deck { get; set; } = default!;

        // All DeckCard entries for this deck, including Card details
        public List<DeckCard> DeckCards { get; set; } = new();

        // Total monetary value of all cards in the deck
        public decimal TotalValue { get; set; }

        // --------------------------------------------------------
        // GET: Load deck details and all cards linked to the deck
        // --------------------------------------------------------
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Retrieve the deck, including:
            //   - DeckCards (relationship table)
            //   - Card (the actual card objects)
            var deck = await _context.Decks
                .Include(d => d.DeckCards)         // Load DeckCard entries
                    .ThenInclude(dc => dc.Card)    // Load the Card linked to each DeckCard
                .FirstOrDefaultAsync(d => d.DeckId == id);

            // If the deck doesn't exist, return a 404 page
            if (deck == null)
            {
                return NotFound();
            }

            // Store data in model properties for the Razor page to display
            Deck = deck;
            DeckCards = deck.DeckCards;

            // ------------------------------
            // Math + Loop: Calculate value
            // ------------------------------
            TotalValue = 0m; // Starting value

            // Loop through each card in the deck and add its value
            foreach (var dc in DeckCards)
            {
                // Handle null values (if a card has no value)
                var cardValue = dc.Card.Value ?? 0m;

                // Add to running total
                TotalValue += cardValue;
            }

            // Render the Razor page
            return Page();
        }
    }
}
