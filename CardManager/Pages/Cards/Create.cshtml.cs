using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class CreateModel : PageModel
    {

        private readonly CardCollectionContext _context;

        public CreateModel(CardCollectionContext context)
        {
            _context = context;
        }

        // BindProperty allows the "Card" object to automatically bind
        // form values from the Razor Page to this property.
        // This is the card we will insert into the database.
        [BindProperty]
        public Card Card { get; set; } = new();

        // OnGet() runs when the Create page is first loaded.
        // No initialization is needed here, but the method is required.
        public void OnGet()
        {
        }


        // OnPostAsync() runs when the form is submitted.
        // It handles validation, business rules, and saving the new card.
        public async Task<IActionResult> OnPostAsync()
        {

            // Ensure Quantity is not null or negative.
            if (Card.Quantity == null || Card.Quantity < 0)
            {
                Card.Quantity = 0;  // Mathematical assignment
            }


            // Ensure the model is valid according to validation attributes.
            // If invalid, redisplay the page with error messages.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add the new card to the EF Core tracking context.
            _context.Cards.Add(Card);

            // Save the new record into the SQLite database asynchronously.
            await _context.SaveChangesAsync();

            // After saving, redirect the user back to the Card Index page.
            return RedirectToPage("./Index");
        }
    }
}