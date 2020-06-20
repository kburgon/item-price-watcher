using System;
using System.Net;
using System.Net.Mail;

namespace ItemPriceWatcher
{
    internal class EmailSender : IDisposable
    {
        private SmtpClient client;
        private readonly string senderEmail;

        /// <summary>
        /// Constructor that sets up the <see cref="SmtpClient"/> object with the given email credentials.
        /// </summary>
        /// <param name="senderEmail">The email address of the account that will send emails.</param>
        /// <param name="senderPassword">The password of the sender email account.</param>
        public EmailSender(string senderEmail, string senderPassword)
        {
            this.senderEmail = senderEmail;
            client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
        }

        /// <summary>
        /// Sends an email to the given <paramref name="toEmailAddress"/> with the given <paramref name="subject"/> and <paramref name="body"/>.
        /// </summary>
        /// <param name="toEmailAddress">The email address to send the message to.</param>
        /// <param name="subject">The subject of the email to send.</param>
        /// <param name="body">The body of the email that is being sent.</param>
        public void SendMail(string toEmailAddress, string subject, string body)
        {
            using var message = new MailMessage(senderEmail, toEmailAddress);
            message.Body = body;
            message.Subject = subject;
            client.Send(message);
        }

        /// <summary>
        /// Disposes <see cref="SmtpClient"/> object.
        /// </summary>
        public void Dispose() => client.Dispose();
    }
}
