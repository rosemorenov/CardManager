using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class EditModel : PageModel
    {
        private readonly CardCollectionContext _context;

        // Constructor receives the EF Core database context via dependency injection
        public EditModel(CardCollectionContext context)
        {
            _context = context;
        }

        // The Card object bound to the form inputs on the Edit.cshtml page
        [BindProperty]
        public Card Card { get; set; } = default!;

        // GET request: Loads the card data for the selected ID into the form
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Attempt to retrieve the card from the database by its primary key
            var card = await _context.Cards.FindAsync(id);

            // If the card does not exist, return a 404 Not Found page
            if (card == null)
            {
                return NotFound();
            }

            // Otherwise, store the card in the model so the form can populate fields
            Card = card;
            return Page();
        }

        // POST request: Saves changes submitted in the Edit form
        public async Task<IActionResult> OnPostAsync()
        {
            // If submitted form values fail validation rules, return to page with errors
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tell EF Core this Card has been modified and should update existing record
            _context.Attach(Card).State = EntityState.Modified;

            try
            {
                // Attempt to save the updated record to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If concurrency failure occurs, check whether record still exists
                if (!_context.Cards.Any(c => c.CardId == Card.CardId))
                {
                    // Card was deleted by someone else before updating → return NotFound
                    return NotFound();
                }
                else
                {
                    // Otherwise rethrow the exception so it can be logged
                    throw;
                }
            }

            // Redirect back to the card list after successful update
            return RedirectToPage("./Index");
        }
    }
}