using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class DeleteModel : PageModel
    {
        // Database context used to interact with the Cards table
        private readonly CardCollectionContext _context;

        // Constructor receives the database context via dependency injection
        public DeleteModel(CardCollectionContext context)
        {
            _context = context;
        }

        // Property bound to the Razor Page; holds the card being deleted
        [BindProperty]
        public Card Card { get; set; } = default!;

        // Handles GET requests to load the delete confirmation page
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the card by ID
            var card = await _context.Cards.FindAsync(id);

            // If no card is found, return a 404
            if (card == null)
            {
                return NotFound();
            }

            // Assign the retrieved card to the bound property for display
            Card = card;

            // Return the delete confirmation page
            return Page();
        }

        // Handles POST requests when the user confirms deletion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Look up the card again to ensure it still exists
            var card = await _context.Cards.FindAsync(id);

            // If the card exists, remove it from the database
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }

            // After deleting, redirect back to the card list page
            return RedirectToPage("./Index");
        }
    }
}