using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class AddCardModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public AddCardModel(CardCollectionContext context)
        {
            _context = context;
        }

        public Deck Deck { get; set; } = default!;
        public List<Card> AllCards { get; set; } = new();

        [BindProperty]
        public int DeckId { get; set; }

        [BindProperty]
        public int SelectedCardId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }

            Deck = deck;
            DeckId = deck.DeckId;

            AllCards = await _context.Cards
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var deck = await _context.Decks.FindAsync(DeckId);
            if (deck == null)
            {
                return NotFound();
            }

            // Simple check if card already in deck
            var existing = await _context.DeckCards
                .FirstOrDefaultAsync(dc => dc.DeckId == DeckId && dc.CardId == SelectedCardId);

            if (existing == null)
            {
                var deckCard = new DeckCard
                {
                    DeckId = DeckId,
                    CardId = SelectedCardId
                };
                _context.DeckCards.Add(deckCard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new { id = DeckId });
        }
    }
}