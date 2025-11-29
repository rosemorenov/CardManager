using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class CreateModel : PageModel
    {
        // EF Core database context injected via dependency injection.
        // This allows the page to add new Deck objects into the database.
        private readonly CardCollectionContext _context;

        // Constructor receives the DbContext instance supplied by DI container.
        public CreateModel(CardCollectionContext context)
        {
            _context = context;
        }

        // Deck object bound to form inputs on the Create.cshtml page.
        // [BindProperty] tells ASP.NET Core to automatically map posted form values into this object.
        [BindProperty]
        public Deck Deck { get; set; } = new();

        // GET handler: Executed when the page first loads.
        // No specific logic needed here since this page only displays the form.
        public void OnGet()
        {
        }

        // POST handler: Runs when the user submits the Create Deck form.
        public async Task<IActionResult> OnPostAsync()
        {
            // -----------------------------
            // MODEL VALIDATION
            // -----------------------------
            // Ensures that:
            //   - The model is valid
            //   - DeckName is not null or empty
            // Demonstrates Boolean operators + conditional logic.
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Deck.DeckName))
            {
                // Adds a manual validation message shown on the page.
                ModelState.AddModelError(string.Empty, "Deck name is required.");
                return Page(); // Return to form with error
            }

            // -----------------------------
            // DATABASE WRITE OPERATION
            // -----------------------------
            // Adds the new Deck to the EF Core change tracker
            _context.Decks.Add(Deck);

            // Saves the new Deck into the SQLite database asynchronously
            await _context.SaveChangesAsync();

            // Redirect back to the deck list page once the deck is created
            return RedirectToPage("./Index");
        }
    }
}