using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.CQRS.ChatMessages.Queries;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.CQRS.ChatSessions.Queries;
using TicketBox.Application.Features.Services;

namespace TicketBox.WebUI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOpenAiChatService _openAiChatService;

        public ChatController(IMediator mediator, IOpenAiChatService openAiChatService)
        {
            _mediator = mediator;
            _openAiChatService = openAiChatService;
        }

        // chatSessionId verilirse (sessionStorage'dan geliyorsa) o oturuma devam eder.
        // Verilmezse (yeni pencere/oturum) HER ZAMAN yeni bir sohbet oturumu açar.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetOrCreateSession(int? chatSessionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (chatSessionId.HasValue)
            {
                var existing = await _mediator.Send(new GetChatSessionByIdQuery { ChatSessionId = chatSessionId.Value });
                if (existing != null && existing.AppUserId == userId)
                {
                    var msgs = await _mediator.Send(new GetChatMessagesBySessionQuery { ChatSessionId = chatSessionId.Value });
                    return Json(new
                    {
                        chatSessionId = existing.ChatSessionId,
                        messages = msgs.OrderBy(m => m.SentDate).Select(m => new { sender = m.Sender, content = m.Content })
                    });
                }
            }

            var newId = await _mediator.Send(new CreateChatSessionCommand { AppUserId = userId });
            return Json(new { chatSessionId = newId, messages = new List<object>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(int chatSessionId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Json(new { success = false, error = "Mesaj boş olamaz." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _mediator.Send(new GetChatSessionByIdQuery { ChatSessionId = chatSessionId });
            if (session == null || session.AppUserId != userId)
                return Json(new { success = false, error = "Geçersiz sohbet oturumu." });

            await _mediator.Send(new CreateChatMessageCommand
            {
                ChatSessionId = chatSessionId,
                Sender = "User",
                Content = message
            });

            var history = await _mediator.Send(new GetChatMessagesBySessionQuery { ChatSessionId = chatSessionId });

            var conversation = new List<(string Role, string Content)>
            {
                ("system",
                    "Sen TicketBox adlı bir bilet satış platformunun müşteri destek asistanısın. " +
                    "Platformun gerçek yapısı şöyle: Kullanıcılar 'Profilim' sayfasından kendi biletlerini, favori " +
                    "etkinliklerini ve yorumlarını görebilir. Aktif bir bileti iptal etmek için Profilim > Biletlerim " +
                    "sekmesinde ilgili biletin altındaki 'İptal Talebi Gönder' butonunu kullanırlar. Etkinlik detay " +
                    "sayfasında rezervasyon yapılır. Türkçe, kısa ve yardımsever cevaplar ver. Bilmediğin konularda " +
                    "dürüst ol ve gerekirse kullanıcıyı canlı destek ekibine yönlendirmeyi öner.")
            };

            foreach (var m in history.OrderBy(m => m.SentDate))
                conversation.Add((m.Sender == "User" ? "user" : "assistant", m.Content));

            string botReply;
            try
            {
                botReply = await _openAiChatService.GetReplyAsync(conversation, HttpContext.RequestAborted);
            }
            catch
            {
                botReply = "Şu anda yanıt veremiyorum, lütfen biraz sonra tekrar deneyin.";
            }

            await _mediator.Send(new CreateChatMessageCommand
            {
                ChatSessionId = chatSessionId,
                Sender = "Bot",
                Content = botReply
            });

            return Json(new { success = true, reply = botReply });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TranscribeAudio(IFormFile audio)
        {
            if (audio == null || audio.Length == 0)
                return Json(new { success = false, error = "Ses kaydı alınamadı." });

            try
            {
                using var stream = audio.OpenReadStream();
                var text = await _openAiChatService.TranscribeAsync(stream, audio.FileName, HttpContext.RequestAborted);
                return Json(new { success = true, text });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Speak([FromForm] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest();

            try
            {
                var audioBytes = await _openAiChatService.GetSpeechAsync(text, HttpContext.RequestAborted);
                return File(audioBytes, "audio/mpeg");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _mediator.Send(new GetChatSessionsByUserQuery { AppUserId = userId });

            foreach (var s in sessions)
                s.Messages = s.Messages.OrderBy(m => m.SentDate).ToList();

            return View(sessions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSession(int chatSessionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _mediator.Send(new GetChatSessionByIdQuery { ChatSessionId = chatSessionId });
            if (session == null || session.AppUserId != userId)
                return Json(new { success = false });

            await _mediator.Send(new DeleteChatSessionCommand { ChatSessionId = chatSessionId });
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int chatMessageId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var message = await _mediator.Send(new GetChatMessageByIdQuery { ChatMessageId = chatMessageId });
            if (message == null)
                return Json(new { success = false });

            var session = await _mediator.Send(new GetChatSessionByIdQuery { ChatSessionId = message.ChatSessionId });
            if (session == null || session.AppUserId != userId)
                return Json(new { success = false });

            await _mediator.Send(new DeleteChatMessageCommand { ChatMessageId = chatMessageId });
            return Json(new { success = true });
        }
    }
}