using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Decks
{
    public class IndexModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public IndexModel(CardCollectionContext context)
        {
            _context = context;
        }

        public List<Deck> Decks { get; set; } = new();

        public async Task OnGetAsync()
        {
            Decks = await _context.Decks
                .Include(d => d.DeckCards)
                .ThenInclude(dc => dc.Card)
                .ToListAsync();
        }
    }
}