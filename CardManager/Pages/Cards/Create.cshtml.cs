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

        [BindProperty]
        public Card Card { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Example of variable, math, and if
            if (Card.Quantity == null || Card.Quantity < 0)
            {
                Card.Quantity = 0;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cards.Add(Card);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}