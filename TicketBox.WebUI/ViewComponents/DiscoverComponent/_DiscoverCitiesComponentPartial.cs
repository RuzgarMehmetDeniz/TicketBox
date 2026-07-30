using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverCitiesComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        // Bilinen büyük şehirler + harita üzerindeki sabit konumları
        private static readonly Dictionary<string, (string Top, string Left)> CityPositions = new()
{
    { "İstanbul", ("20%", "23%") },
    { "Ankara",   ("38%", "38%") },
    { "İzmir",    ("60%", "16%") },
    { "Antalya",  ("83%", "27%") },
    { "Bursa",    ("33%", "21%") },
    { "Adana",    ("82%", "51%") },
    { "Konya",    ("68%", "36%") },
};

        private readonly TicketContext _context2; // (kullanılmıyor, silinecek satır - aşağıda düzeltildi)

        public _DiscoverCitiesComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var locations = await _context.Events
                .Where(e => e.IsActive && e.EventDate >= DateTime.Now)
                .Select(e => e.Location)
                .ToListAsync();

            var cityCounts = new Dictionary<string, int>();

            foreach (var location in locations)
            {
                if (string.IsNullOrWhiteSpace(location))
                    continue;

                var matchedCity = CityPositions.Keys
                    .FirstOrDefault(city => location.Contains(city, StringComparison.OrdinalIgnoreCase));

                if (matchedCity != null)
                {
                    cityCounts[matchedCity] = cityCounts.GetValueOrDefault(matchedCity) + 1;
                }
            }

            var result = cityCounts
                .OrderByDescending(kv => kv.Value)
                .Take(6)
                .Select(kv => new CityMapItemViewModel
                {
                    CityName = kv.Key,
                    EventCount = kv.Value,
                    Top = CityPositions[kv.Key].Top,
                    Left = CityPositions[kv.Key].Left
                })
                .ToList();

            return View(result);
        }

      
    }
}