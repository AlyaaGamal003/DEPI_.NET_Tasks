using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //  Create an async email sender that sends multiple emails concurrently with retry logic.
    internal class EmailSender
    {
        private readonly SmtpClient _smtpClient;

        public EmailSender(string host, int port)
        {
            _smtpClient = new SmtpClient(host, port);
            
            // _smtpClient.Credentials = new System.Net.NetworkCredential("username", "password");
            // _smtpClient.EnableSsl = true;
        }

        
        public async Task SendEmailAsync(string from, string to, string subject, string body, int maxRetries = 3)
        {
            int attempt = 0;
            while (attempt < maxRetries)
            {
                try
                {
                    attempt++;
                    var mail = new MailMessage(from, to, subject, body);
                    await _smtpClient.SendMailAsync(mail);
                    Console.WriteLine($"Email sent to {to}");
                    return; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email to {to}, attempt {attempt}: {ex.Message}");
                    await Task.Delay(1000); 
                }
            }

            Console.WriteLine($"Failed to send email to {to} after {maxRetries} attempts.");
        }

        public async Task SendEmailsAsync(List<(string From, string To, string Subject, string Body)> emails)
        {
            var tasks = new List<Task>();
            foreach (var email in emails)
            {
                tasks.Add(SendEmailAsync(email.From, email.To, email.Subject, email.Body));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("All emails processed.");
        }
    }
}
