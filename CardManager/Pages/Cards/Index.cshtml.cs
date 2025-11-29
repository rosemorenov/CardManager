using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CardManager.Data;
using CardManager.Models;

namespace CardManager.Pages.Cards
{
    public class IndexModel : PageModel
    {
        private readonly CardCollectionContext _context;

        public IndexModel(CardCollectionContext context)
        {
            _context = context;
        }

        public List<Card> Cards { get; set; } = new();

        // Simple search term to show Boolean/if/loop usage
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync(string? search)
        {
            SearchTerm = search;

            IQueryable<Card> query = _context.Cards;

            // Boolean operators + conditionals
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.Name.Contains(search) ||
                    c.CardSet.Contains(search) ||
                    c.Rarity.Contains(search));
            }

            // Loop will be used in the Razor view to render rows
            Cards = await query
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}