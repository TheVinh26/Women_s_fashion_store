using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace ShopDoNu.Library
{
    public class EmailSender
    {
        private const string SendGridApiKey = "";
        private const string SenderEmail = "maithevinh2723@gmail.com";
        private const string SenderName = "TheVinh";

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var client = new SendGridClient(SendGridApiKey);

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(SenderEmail, SenderName),
                    Subject = subject,
                    PlainTextContent = body,
                    HtmlContent = body
                };

                msg.AddTo(new EmailAddress(toEmail));

                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's requirements
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if needed
            }
        }
    }
}