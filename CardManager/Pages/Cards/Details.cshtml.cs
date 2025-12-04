using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class DetailsModel : PageModel
    {
 
        // Used to query the Cards table
        private readonly CardCollectionContext _context;

        // Constructor receives the database context through dependency injection
        public DetailsModel(CardCollectionContext context)
        {
            _context = context;
        }

        // Holds the card that will be displayed on the Details page
        // "default!" tells the compiler we will initialize it before use
        public Card Card { get; set; } = default!;

        // GET: /Cards/Details?id=###
        // Retrieves the card with the given ID from DB
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Attempt to find the card by its primary key
            var card = await _context.Cards.FindAsync(id);

            // If not found, return a 404 response
            if (card == null)
            {
                return NotFound();
            }

            // Store the card in the public property for Razor to display
            Card = card;

            // Render the Razor Page
            return Page();
        }
    }
}