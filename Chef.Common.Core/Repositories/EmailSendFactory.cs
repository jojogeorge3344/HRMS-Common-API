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
        Task<int> SendApprovedEmailAsync(MailRequest mailRequest);
        Task <int>SendReplayedEmailAsync(MailRequest mailRequest);
        Task <int>SendRequestedEmailAsync(MailRequest mailRequest);
    }

    public class EmailSendFactory : IEmailSendFactory
    {
        //private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment Webenv;
        private readonly MailSettings _mailSettings;
        private IConfiguration Configuration;
        public EmailSendFactory(IOptions<MailSettings> mailSettings, IConfiguration _configuration,IWebHostEnvironment _env)
        {
            _mailSettings = mailSettings.Value;
            Configuration = _configuration;
            Webenv = _env;
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

                    ////if (_env.IsDevelopment())
                    ////{
                    ////    // The third parameter is useSSL (true if the client should make an SSL-wrapped
                    ////    // connection to the server; otherwise, false).
                    ////   // await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                        
                    ////}
                    ////else
                    ////{
                    ////   // await client.ConnectAsync(_emailSettings.MailServer);
                    ////}

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
         public async Task<int> SendApprovedEmailAsync(MailRequest request)
        {
            var Result = 0;
            string FilePath = request.TemplateFilepath+"\\EmailTemplate.html";
              StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/api/approvalsystem")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName)
            .Replace("[Approvedusername]",request.ApprovedBy);
            //Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(request.UserEmail);
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
               
                email.Body = builder.ToMessageBody();
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
                var host = this.Configuration.GetSection("MailSettings")["Host"];
                int Port = Convert.ToInt32(this.Configuration.GetSection("MailSettings")["Port"]);
                var Mail = this.Configuration.GetSection("MailSettings")["Email"];
                var Password = this.Configuration.GetSection("MailSettings")["Password"];
                var Issend = Convert.ToBoolean(this.Configuration.GetSection("MailSettings")["isMailSend"]);
                smtp.Connect("smtp.office365.com", Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(Mail, Password);
                if (Issend)
                {
                    await smtp.SendAsync(email);
                    await SendReplayedEmailAsync(request);
                    Result = 1;
                }
                else
                    Result = 0;

                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                throw;
            }
            return Result;


        }
        public async Task<int> SendRequestedEmailAsync(MailRequest request)
        {
            var Result = 0;
            var maSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
           
           string FilePath = request.TemplateFilepath ;
          
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName);
           
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(request.UserEmail);
            
            foreach (var toemail in request.ToEmail)
            {
                email.To.Add(MailboxAddress.Parse(toemail));
            }
            email.Subject = request.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
          
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
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
                var host = this.Configuration.GetSection("MailSettings")["Host"]; 
                int Port = Convert.ToInt32(this.Configuration.GetSection("MailSettings")["Port"]);
                var Mail = this.Configuration.GetSection("MailSettings")["Email"];
                var Password = this.Configuration.GetSection("MailSettings")["Password"];
                var Issend = Convert.ToBoolean(this.Configuration.GetSection("MailSettings")["isMailSend"]);
                smtp.Connect("smtp.office365.com", Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(Mail,Password);
                if (Issend)
                {
                    await smtp.SendAsync(email);
                    Result = 1;
                }
                else
                    Result = 0;

                smtp.Disconnect(true);
              
            }
            catch(Exception ex)
            {
                throw;
            }
            return Result;
        }
        public async Task<int> SendReplayedEmailAsync(MailRequest request)
        {
            var Result = 0;
            var maSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();

            string FilePath = request.TemplateFilepath;
            FilePath = FilePath + "\\ReplyEmail.html";

            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[URL]", "https://192.168.100.54:9034/")
            .Replace("[requestdate]", request.DocumentDate).Replace("[documentnumber]", request.DocumentNumber).Replace("[documentname]", request.DocumentName)
            .Replace("[Approvedusername]",request.ApprovedBy);
           
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(request.UserEmail);
            
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
           
            email.Body = builder.ToMessageBody();
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                var mailSettings = this.Configuration.GetSection("MailSettings").Get<MailSettings>();
                var host = this.Configuration.GetSection("MailSettings")["Host"];
                int Port = Convert.ToInt32(this.Configuration.GetSection("MailSettings")["Port"]);
                var Mail = this.Configuration.GetSection("MailSettings")["Email"];
                var Password = this.Configuration.GetSection("MailSettings")["Password"];
                var Issend = Convert.ToBoolean(this.Configuration.GetSection("MailSettings")["isMailSend"]);
                smtp.Connect("smtp.office365.com", Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(Mail, Password);
                if (Issend)
                {
                    await smtp.SendAsync(email);
                    Result = 1;
                }
                else
                    Result = 0;
                smtp.Disconnect(true);
              
            }
            catch(Exception ex)
            {
                throw;
            }
            return Result;
        }
    }
}
