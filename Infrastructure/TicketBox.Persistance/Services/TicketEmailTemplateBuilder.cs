using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Services;

namespace TicketBox.Persistance.Services
{
    public static class TicketEmailTemplateBuilder
    {
        private static readonly CultureInfo TrCulture = new("tr-TR");

        public static string Build(TicketEmailModel model)
        {
            var eventDate = model.EventDate.ToString("dd MMM yyyy", TrCulture);
            var price = model.Price.ToString("N2", TrCulture);

            return $@"
<!DOCTYPE html>
<html lang=""tr"">
<body style=""margin:0;padding:0;background:#0d0f16;"">
    <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#0d0f16;padding:40px 0;"">
        <tr>
            <td align=""center"">
                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""460"" style=""max-width:92%;background:#161a24;border:1px solid #2a2f3d;border-radius:16px;font-family:'Segoe UI',Arial,sans-serif;color:#f5f6fa;"">
                    <tr>
                        <!-- Sol: bilet ana bilgileri -->
                        <td width=""300"" valign=""top"" style=""padding:28px 26px;"">
                            <span style=""display:inline-block;font-size:11px;letter-spacing:.08em;text-transform:uppercase;color:#7c5cff;background:rgba(255,255,255,.05);padding:4px 12px;border-radius:100px;"">{model.Status}</span>
                            <h2 style=""font-size:19px;line-height:1.3;margin:16px 0 20px;"">{model.EventTitle}</h2>
 
                            <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-size:13.5px;"">
                                <tr>
                                    <td style=""padding-bottom:12px;border-bottom:1px solid #232838;color:#9098ab;"">Ad Soyad</td>
                                    <td style=""padding-bottom:12px;border-bottom:1px solid #232838;text-align:right;font-weight:600;"">{model.CustomerFullName}</td>
                                </tr>
                                <tr>
                                    <td style=""padding:12px 0;border-bottom:1px solid #232838;color:#9098ab;"">Etkinlik Tarihi</td>
                                    <td style=""padding:12px 0;border-bottom:1px solid #232838;text-align:right;font-weight:600;"">{eventDate}</td>
                                </tr>
                                <tr>
                                    <td style=""padding-top:12px;color:#9098ab;"">Fiyat</td>
                                    <td style=""padding-top:12px;text-align:right;font-weight:600;"">₺{price}</td>
                                </tr>
                            </table>
                        </td>
 
                        <!-- Sağ: koçan / PNR -->
                        <td width=""150"" valign=""middle"" align=""center"" style=""border-left:2px dashed #2a2f3d;padding:24px 14px;"">
                            <div style=""width:76px;height:76px;margin:0 auto 10px;background:rgba(255,255,255,.05);border:1px solid #2a2f3d;border-radius:12px;line-height:76px;font-size:34px;text-align:center;"">▤</div>
                            <b style=""display:block;font-family:'Courier New',monospace;color:#7c5cff;letter-spacing:.05em;font-size:14px;word-break:break-all;"">{model.PNRCode}</b>
                            <span style=""display:block;font-size:10.5px;text-transform:uppercase;letter-spacing:.08em;color:#9098ab;margin-top:6px;"">PNR Kodu</span>
                        </td>
                    </tr>
                </table>
 
                <p style=""color:#9098ab;font-size:12px;margin-top:16px;font-family:'Segoe UI',Arial,sans-serif;"">
                    TicketBox — Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayınız.
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}
