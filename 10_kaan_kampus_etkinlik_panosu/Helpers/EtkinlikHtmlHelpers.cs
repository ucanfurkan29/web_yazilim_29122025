using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _10_kaan_kampus_etkinlik_panosu.Helpers
{
    public static class EtkinlikHtmlHelpers
    {
        public static IHtmlContent EtkinlikTarihi(this IHtmlHelper html, DateTime tarih)
        {
            return new HtmlString($"<span class='badge bg-secondary'>{tarih:dd MMMM yyyy}</span>");
        }
    }
}
