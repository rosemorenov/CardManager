using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class DeleteModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public DeleteModel(CardCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Card Card { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            Card = card;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}