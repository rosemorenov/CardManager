using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class RemoveCardModel : PageModel
    {
        private readonly CardCollectionContext _context;

        // Constructor injection of the EF Core database context
        public RemoveCardModel(CardCollectionContext context)
        {
            _context = context;
        }

        // This property will store the DeckCard record shown on the confirmation page.
        // BindProperty allows Razor form POST to map values back to this property.
        [BindProperty]
        public DeckCard DeckCard { get; set; } = default!;


        // GET: Called when user navigates to /Decks/RemoveCard?id=123
        // Loads DeckCard record, including related Card and Deck objects.
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the DeckCard entry with the matching ID.
            // Include Deck + Card so the Razor Page can show both names.
            var deckCard = await _context.DeckCards
                .Include(dc => dc.Deck)
                .Include(dc => dc.Card)
                .FirstOrDefaultAsync(dc => dc.DeckCardId == id);

            // If the DeckCard doesn't exist, show a 404 error.
            if (deckCard == null)
            {
                return NotFound();
            }

            // Save this entity so the Razor page can render it.
            DeckCard = deckCard;
            return Page();
        }


        // POST: User clicks "Remove" button
        // Removes the selected card from the deck

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Lookup the DeckCard again for safety.
            // Only include Deck (not Card) because we only need DeckId.
            var deckCard = await _context.DeckCards
                .Include(dc => dc.Deck)
                .FirstOrDefaultAsync(dc => dc.DeckCardId == id);

            // If nothing found, return 404.
            if (deckCard == null)
            {
                return NotFound();
            }

            // Save deck ID so we can redirect properly after deletion.
            var deckId = deckCard.DeckId;

            // Remove the DeckCard relationship (NOT the card or the deck itself).
            _context.DeckCards.Remove(deckCard);
            await _context.SaveChangesAsync();

            // Redirect back to the Deck Details page
            return RedirectToPage("./Details", new { id = deckId });
        }
    }
}
