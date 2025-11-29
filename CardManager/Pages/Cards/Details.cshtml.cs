using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class DetailsModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public DetailsModel(CardCollectionContext context)
        {
            _context = context;
        }

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
    }
}