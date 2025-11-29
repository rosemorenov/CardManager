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

        public EditModel(CardCollectionContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cards.Any(c => c.CardId == Card.CardId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}