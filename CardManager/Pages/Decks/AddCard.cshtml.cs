using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class AddCardModel : PageModel
    {
        private readonly CardCollectionContext _context;

        // Constructor - receives the database context via dependency injection
        public AddCardModel(CardCollectionContext context)
        {
            _context = context;
        }

        // Properties Used for Page State

        // DeckId is bound from the query string (?deckId=1)
        // SupportsGet=true allows Razor to bind this value for GET requests
        [BindProperty(SupportsGet = true)]
        public int DeckId { get; set; }

        // Stores the selected card when the user submits the form
        [BindProperty]
        public int SelectedCardId { get; set; }

        // Holds the deck being modified
        public Deck? Deck { get; set; }

        // List of all cards used to populate the dropdown
        public List<Card> AllCards { get; set; } = new();

        // Holds the selected card details for previewing on the page
        public Card? CardPreview { get; set; }

        // GET: Load Deck and Card List
        public async Task<IActionResult> OnGetAsync()
        {
            // Ensure a valid DeckId is supplied
            if (DeckId <= 0)
                return NotFound("Missing deckId");

            // Load the deck from the database
            Deck = await _context.Decks.FirstOrDefaultAsync(d => d.DeckId == DeckId);

            // If no deck exists with this ID, return 404
            if (Deck == null)
                return NotFound("Deck not found");

            // Load all available cards for the dropdown
            AllCards = await _context.Cards
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Page();
        }


        // POST: ShowDetails - Display selected card info
        // Triggered when user clicks the "Show Details" button
        public async Task<IActionResult> OnPostShowDetailsAsync()
        {
            // Reload dropdown list and deck data
            await OnGetAsync();

            // Retrieve selected card information for preview
            CardPreview = await _context.Cards
                .FirstOrDefaultAsync(c => c.CardId == SelectedCardId);

            return Page();
        }

        // POST: AddCard - Add the selected card to the deck
        // Triggered when user clicks "Add Card to Deck"

        public async Task<IActionResult> OnPostAddCardAsync()
        {
            // Validate inputs
            if (DeckId <= 0 || SelectedCardId <= 0)
                return RedirectToPage("./Index");

            // Create new DeckCard (join table entry)
            var deckCard = new DeckCard
            {
                DeckId = DeckId,
                CardId = SelectedCardId
            };

            // Save the new relationship to the database
            _context.DeckCards.Add(deckCard);
            await _context.SaveChangesAsync();

            // Redirect back to the deck details page
            return RedirectToPage("./Details", new { id = DeckId });
        }
    }
}