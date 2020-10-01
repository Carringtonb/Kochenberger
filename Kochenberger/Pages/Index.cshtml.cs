using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Kochenberger.Pages
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public EmailMessage email { get; set; }

        public void OnGet()
        {

        }
        public async Task OnPostSendEmail()
        {
            using (var smtp = new SmtpClient("Your SMTP server address"))
            {
                var emailMessage = new MailMessage();
                emailMessage.From = new MailAddress(email.From);
                emailMessage.To.Add(email.To);
                emailMessage.Subject = email.Subject;
                emailMessage.Body = email.Body;

                await smtp.SendMailAsync(emailMessage);
            }
        }
    }

}
