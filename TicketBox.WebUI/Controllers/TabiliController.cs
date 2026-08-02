using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TicketBox.Application.Features.Services;

namespace TicketBox.WebUI.Controllers
{
    public class TabiliController : Controller
    {
        private readonly IOpenAiChatService _openAiChatService;
        private readonly ITabiliAnalyticsService _analyticsService;

        public TabiliController(IOpenAiChatService openAiChatService, ITabiliAnalyticsService analyticsService)
        {
            _openAiChatService = openAiChatService;
            _analyticsService = analyticsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ask([FromForm] string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return Json(new { success = false, error = "Soru boş olamaz." });

            var tools = BuildTools();

            var now = DateTime.Now;

            var systemPrompt =
                $"Sen TicketBox platformunun 'Tabili AI' adlı iç analiz asistanısın. Yöneticiye, platformun " +
                "gerçek satış ve kullanım verileriyle ilgili sorularını cevaplıyorsun. " +
                $"BUGÜNÜN GERÇEK TARİHİ: {now:yyyy-MM-dd} ({now:dd MMMM yyyy, dddd}). " +
                "Kullanıcı 'bu ay', 'geçen ay', 'bu yıl' gibi göreceli zaman ifadeleri kullandığında, HER ZAMAN " +
                "yukarıdaki gerçek tarihi baz alarak hesapla — kendi bildiğin veya tahmin ettiğin bir tarihi ASLA kullanma. " +
                "Sana verilen araçları kullanarak gerçek veriyi çek, ardından bu veriyi kısa, net ve sayısal olarak Türkçe özetle. " +
                "Asla veri uydurma; elindeki araçların dışında bir bilgin yoksa bunu belirt.";

            try
            {
                var answer = await _openAiChatService.AskWithToolsAsync(systemPrompt, question, tools, HttpContext.RequestAborted);
                return Json(new { success = true, answer });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        private List<ToolDefinition> BuildTools()
        {
            return new List<ToolDefinition>
            {
                new ToolDefinition
                {
                    Name = "get_total_revenue",
                    Description = "Belirli bir tarih aralığındaki (veya tüm zamanların) toplam onaylanmış ödeme gelirini döndürür.",
                    ParametersSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            from = new { type = "string", format = "date", description = "Başlangıç tarihi (opsiyonel), YYYY-MM-DD formatında" },
                            to = new { type = "string", format = "date", description = "Bitiş tarihi (opsiyonel), YYYY-MM-DD formatında" }
                        }
                    },
                    Execute = async (args) =>
                    {
                        System.DateTime? from = args.TryGetProperty("from", out var f) && f.ValueKind == JsonValueKind.String
                            ? System.DateTime.Parse(f.GetString()!) : null;
                        System.DateTime? to = args.TryGetProperty("to", out var t) && t.ValueKind == JsonValueKind.String
                            ? System.DateTime.Parse(t.GetString()!) : null;

                        var total = await _analyticsService.GetTotalRevenueAsync(from, to);
                        return JsonSerializer.Serialize(new { totalRevenue = total });
                    }
                },
                new ToolDefinition
                {
                    Name = "get_best_selling_events",
                    Description = "En çok bilet satan etkinlikleri, satış adedi ve toplam geliriyle birlikte listeler.",
                    ParametersSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            top = new { type = "integer", description = "Kaç etkinlik listelensin (varsayılan 5)" }
                        }
                    },
                    Execute = async (args) =>
                    {
                        int top = args.TryGetProperty("top", out var tp) && tp.ValueKind == JsonValueKind.Number ? tp.GetInt32() : 5;
                        var result = await _analyticsService.GetBestSellingEventsAsync(top);
                        return JsonSerializer.Serialize(result);
                    }
                },
                new ToolDefinition
                {
                    Name = "get_revenue_by_category",
                    Description = "Kategori bazında toplam bilet sayısı ve geliri döndürür (Konser, Tiyatro, Spor vb.).",
                    ParametersSchema = new { type = "object", properties = new { } },
                    Execute = async (_) =>
                    {
                        var result = await _analyticsService.GetRevenueByCategoryAsync();
                        return JsonSerializer.Serialize(result);
                    }
                },
                new ToolDefinition
                {
                    Name = "get_monthly_ticket_trend",
                    Description = "Son N aydaki aylık bilet satış adedi ve gelir trendini döndürür.",
                    ParametersSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            months = new { type = "integer", description = "Kaç ay geriye gidilsin (varsayılan 6)" }
                        }
                    },
                    Execute = async (args) =>
                    {
                        int months = args.TryGetProperty("months", out var m) && m.ValueKind == JsonValueKind.Number ? m.GetInt32() : 6;
                        var result = await _analyticsService.GetMonthlyTicketTrendAsync(months);
                        return JsonSerializer.Serialize(result);
                    }
                },
                new ToolDefinition
                {
                    Name = "get_refund_stats",
                    Description = "Toplam iade talebi sayısını, onaylanan/bekleyen/reddedilen dağılımını ve toplam iade edilen tutarı döndürür.",
                    ParametersSchema = new { type = "object", properties = new { } },
                    Execute = async (_) =>
                    {
                        var result = await _analyticsService.GetRefundStatsAsync();
                        return JsonSerializer.Serialize(result);
                    }
                },
                new ToolDefinition
                {
                    Name = "get_new_users_count",
                    Description = "Belirli bir tarih aralığında (veya tüm zamanlarda) kayıt olan yeni kullanıcı sayısını döndürür.",
                    ParametersSchema = new
                    {
                        type = "object",
                        properties = new
                        {
                            from = new { type = "string", format = "date" },
                            to = new { type = "string", format = "date" }
                        }
                    },
                    Execute = async (args) =>
                    {
                        System.DateTime? from = args.TryGetProperty("from", out var f) && f.ValueKind == JsonValueKind.String
                            ? System.DateTime.Parse(f.GetString()!) : null;
                        System.DateTime? to = args.TryGetProperty("to", out var t) && t.ValueKind == JsonValueKind.String
                            ? System.DateTime.Parse(t.GetString()!) : null;

                        var count = await _analyticsService.GetNewUsersCountAsync(from, to);
                        return JsonSerializer.Serialize(new { newUsersCount = count });
                    }
                },
                new ToolDefinition
                {
                    Name = "get_coupon_usage",
                    Description = "Tüm kuponların kullanım sayısını, indirim oranını ve kullanım limitini listeler.",
                    ParametersSchema = new { type = "object", properties = new { } },
                    Execute = async (_) =>
                    {
                        var result = await _analyticsService.GetCouponUsageAsync();
                        return JsonSerializer.Serialize(result);
                    }
                }
            };
        }
    }
}