using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Chef.Common.Core;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MailKit.Security;

namespace Chef.Common.Repositories
{
    

    
    public interface IEmailSendFactory
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<int> SendMail(string message,string status, List<string> email,string ccmail,string frommail,string subject);
        Task<int> SendApprovedEmailAsync(MailRequest mailRequest);
        Task <int>SendReplayedEmailAsync(MailRequest mailRequest);
        Task <int>SendRequestedEmailAsync(MailRequest mailRequest);
    }

    public class EmailSendFactory : IEmailSendFactory
    {

        //private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;
        private readonly MailSettings _mailSettings;
        private IConfiguration Configuration;
        public EmailSendFactory(IOptions<MailSettings> mailSettings, IConfiguration _configuration)
        {
            _mailSettings = mailSettings.Value;
            Configuration = _configuration;
        }
        //public EmailSendFactory(
        //    IOptions<EmailSettings> emailSettings,
        //    IHostingEnvironment env)
        //{
        //    _emailSettings = emailSettings.Value;
        //    _env = env;
        //}

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

             //   mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));

                mimeMessage.To.Add(new MailboxAddress("", email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                       // await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                        
                    }
                    else
                    {
                       // await client.ConnectAsync(_emailSettings.MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                  //  await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<int> SendMail(string message, string status, List<string> email, string ccmail, string frommail, string subject)
        {
            MailAddress fromAddress = new MailAddress("athirak@thomsuninfocare.com");
            MailAddress toAddress = new MailAddress("deepak@thomsuninfocare.com");


            // string FilePath = "C:\\PROJECT\\chef.common\\Chef.Common.Core\\EmailTemplate.html";
            //StreamReader str = new StreamReader(FilePath);
            //string MailText = str.ReadToEnd();
            //str.Close();
            if (status == "2") status = "approved"; else status = "sendforrequest";
            //Repalce [newusername] = signup user name   
            //MailText = MailText.Replace("[newusername]","athira");
            //MailText = MailText.Replace("[status]", status);
            //MailText=MailText.Replace("[documentid]",message);


            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //client.Host = ConfigurationManager.AppSetting["Host"];
            //client.Port = Convert.ToInt32(ConfigurationManager.AppSetting["Port"]);
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential("athirak@thomsuninfocare.com", "Aswina@1");


            try
            {
                //foreach (string email_to in email)
                //{
                //    MailAddress toAddress = new MailAddress(email_to);

                MailMessage mail = new MailMessage(fromAddress.Address, toAddress.Address);

                mail.IsBodyHtml = true;
                mail.CC.Add(new MailAddress(ccmail));
                mail.Subject = subject;
                mail.Body = message;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                client.Send(mail);
                //}


            }
            catch (Exception ex)
            {

            }
            return 1;
        }
        public async Task<int> SendApprovedEmailAsync(MailRequest request)
        {
            string FilePath = "C:\\PROJECT\\chef.common\\Chef.Common.Core\\EmailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/api/approvalsystem")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName)
            .Replace("[Approvedusername]",request.ApprovedBy);
            //Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("athirak @thomsuninfocare.com");
            //MailboxAddress.Parse(_mailSettings.Mail);
            foreach (var toemail in request.ToEmail)
            {
                email.To.Add(MailboxAddress.Parse(toemail));
            }
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                //email.Body = builder.ToMessageBody();

                if (request.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in request.Attachments)
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
                //builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
                var host = this.Configuration.GetSection("MailSettings")["Host"];
                var Port = this.Configuration.GetSection("MailSettings")["Port"];
                var Mail = this.Configuration.GetSection("MailSettings")["Mail"];
                var Password = this.Configuration.GetSection("MailSettings")["Password"];
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("athirak@thomsuninfocare.com", "Aswina@1");
                await smtp.SendAsync(email);

                smtp.Disconnect(true);
            await SendReplayedEmailAsync(request);
            return 1;


        }
        public async Task<int> SendRequestedEmailAsync(MailRequest request)
        {
            string FilePath = "C:\\PROJECT\\chef.common\\Chef.Common.Core\\SendRequestemail.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName);
            //Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("athirak @thomsuninfocare.com");
            //MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse("deepakp@thomsuninfocare.com"));
            foreach (var toemail in request.ToEmail)
            {
                email.To.Add(MailboxAddress.Parse(toemail));
            }
            email.Subject = request.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            //email.Body = builder.ToMessageBody();

            if (request.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in request.Attachments)
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
            //builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
            var host = this.Configuration.GetSection("MailSettings")["Host"];
            var Port = this.Configuration.GetSection("MailSettings")["Port"];
            var Mail = this.Configuration.GetSection("MailSettings")["Mail"];
            var Password = this.Configuration.GetSection("MailSettings")["Password"];
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("athirak@thomsuninfocare.com", "Aswina@1");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return 1;
        }
        public async Task<int> SendReplayedEmailAsync(MailRequest request)
        {
            string FilePath = "C:\\PROJECT\\chef.common\\Chef.Common.Core\\ReplyEmail.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName)
            .Replace("[Approvedusername]",request.ApprovedBy);
            //Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("athirak @thomsuninfocare.com");
            //MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse("deepakp@thomsuninfocare.com"));
            foreach (var toemail in request.ToEmail)
            {
                email.To.Add(MailboxAddress.Parse(toemail));
            }
            email.Subject = "Approval Notification";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            //email.Body = builder.ToMessageBody();

            if (request.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in request.Attachments)
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
            //builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
            var host = this.Configuration.GetSection("MailSettings")["Host"];
            var Port = this.Configuration.GetSection("MailSettings")["Port"];
            var Mail = this.Configuration.GetSection("MailSettings")["Mail"];
            var Password = this.Configuration.GetSection("MailSettings")["Password"];
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("athirak@thomsuninfocare.com", "Aswina@1");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return 1;
        }
    }
}
