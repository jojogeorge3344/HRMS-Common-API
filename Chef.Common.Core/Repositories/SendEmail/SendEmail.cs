using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Chef.Common.Core.Repositories
{
    public interface ISendEmail
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }

    public class SendEmail : ISendEmail
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;

        public SendEmail(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_emailSettings.SenderEmail);
            //  email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;

                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.MailServer, _emailSettings.MailPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SenderEmail, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}