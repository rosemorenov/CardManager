using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class RemoveCardModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public RemoveCardModel(CardCollectionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeckCard DeckCard { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var deckCard = await _context.DeckCards
                .Include(dc => dc.Deck)
                .Include(dc => dc.Card)
                .FirstOrDefaultAsync(dc => dc.DeckCardId == id);

            if (deckCard == null)
            {
                return NotFound();
            }

            DeckCard = deckCard;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var deckCard = await _context.DeckCards
                .Include(dc => dc.Deck)
                .FirstOrDefaultAsync(dc => dc.DeckCardId == id);

            if (deckCard == null)
            {
                return NotFound();
            }

            var deckId = deckCard.DeckId;

            _context.DeckCards.Remove(deckCard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = deckId });
        }
    }
}