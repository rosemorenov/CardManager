using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class CreateModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public CreateModel(CardCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deck Deck { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Deck.DeckName))
            {
                ModelState.AddModelError(string.Empty, "Deck name is required.");
                return Page();
            }

            _context.Decks.Add(Deck);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}