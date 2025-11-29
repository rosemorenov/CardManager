using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class DeleteModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public DeleteModel(CardCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deck Deck { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }

            Deck = deck;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var deck = await _context.Decks
                .Include(d => d.DeckCards)
                .FirstOrDefaultAsync(d => d.DeckId == id);

            if (deck != null)
            {
                _context.DeckCards.RemoveRange(deck.DeckCards);
                _context.Decks.Remove(deck);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}