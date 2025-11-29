using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class DeleteModel : PageModel
    {
        // The database context used to interact with SQLite
        private readonly CardCollectionContext _context;

        public DeleteModel(CardCollectionContext context)
        {
            _context = context;
        }

        // Holds the deck being deleted.
        // BindProperty allows this object to retain its values across GET and POST requests.
        [BindProperty]
        public Deck Deck { get; set; } = default!;

        // ------------------------------------------------------
        // GET REQUEST — Display confirmation page
        // ------------------------------------------------------
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Look up the deck by its primary key
            var deck = await _context.Decks.FindAsync(id);

            // If the deck does not exist, return a 404 error
            if (deck == null)
            {
                return NotFound();
            }

            // Store the deck in the bound property for display on the page
            Deck = deck;

            // Return the Razor Page to the user
            return Page();
        }

        // ------------------------------------------------------
        // POST REQUEST — Perform the delete
        // ------------------------------------------------------
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Retrieve the deck INCLUDING its related DeckCard entries
            // so that they can be deleted first (due to FK constraints)
            var deck = await _context.Decks
                .Include(d => d.DeckCards)
                .FirstOrDefaultAsync(d => d.DeckId == id);

            // If the deck exists, delete it and its related records
            if (deck != null)
            {
                // Remove all related DeckCards first to avoid foreign key errors
                _context.DeckCards.RemoveRange(deck.DeckCards);

                // Now remove the deck itself
                _context.Decks.Remove(deck);

                // Save all database changes
                await _context.SaveChangesAsync();
            }

            // Redirect back to the list of decks after deletion
            return RedirectToPage("./Index");
        }
    }
}
