using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class DetailsModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public DetailsModel(CardCollectionContext context)
        {
            _context = context;
        }

        public Deck Deck { get; set; } = default!;
        public List<DeckCard> DeckCards { get; set; } = new();
        public decimal TotalValue { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var deck = await _context.Decks
                .Include(d => d.DeckCards)
                    .ThenInclude(dc => dc.Card)
                .FirstOrDefaultAsync(d => d.DeckId == id);

            if (deck == null)
            {
                return NotFound();
            }

            Deck = deck;
            DeckCards = deck.DeckCards;

            // Math + loops
            TotalValue = 0m;
            foreach (var dc in DeckCards)
            {
                var cardValue = dc.Card.Value ?? 0m;
                TotalValue += cardValue;
            }

            return Page();
        }
    }
}