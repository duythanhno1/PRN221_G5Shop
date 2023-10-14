using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BAL.Helpers {
    public class EmailHelper {
        private readonly ILogger<EmailHelper> _logger;
        public EmailHelper(ILogger<EmailHelper> logger) => _logger = logger;

        public EmailHelper() {
        }

        public bool SendEmailTwoFactorCode(string userEmail, string code) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            try {
                //client.Send(mailMessage);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
                email.To.Add(MailboxAddress.Parse(userEmail));
                email.Subject = "Login 2 factor code";
                email.Body = new TextPart(TextFormat.Html) {
                    Text = $"<h1>This is your 2 factor login code </h1>" +
                    $"<h3>{code}</h3>"
                };

                // send email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex) {
                // log exception
                _logger.LogError(ex.Message);
            }
            return false;
        }

        public bool SendEmail(string userEmail, string confirmationLink) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Confirm your email";
            email.Body = new TextPart(TextFormat.Html) {
                Text = "<h1>Welcome to my store</h1>" +
                "<span> Click this link to confirm your email: </span>" +
                $"<a href='{confirmationLink}'> Confirm </a>"
            };

            // send email
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");
            smtp.Send(email);
            smtp.Disconnect(true);
            return false;
        }

        public bool SendEmailPasswordReset(string userEmail, string link) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Reset your password";
            email.Body = new TextPart(TextFormat.Html) {
                Text =
                "<h1>Reset password</h1>" +
                "<span>Click this link to reset your password </span>" +
                    $"<a href='{link}'>Reset password</a>"
            };

            // send email
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");

            try {
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex) {
                // log exception
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public bool SendEmailPasswordInit(string userEmail, string password) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Hello";
            email.Body = new TextPart(TextFormat.Html) {
                Text =
                "<h1>Welcome to my store</h1>" +
                "<span>You can reset your password later: </span>" +
                    $"{password}"
            };

            // send email
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");

            try {
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex) {
                // log exception
                _logger.LogError(ex.Message);
            }
            return false;
        }
    }
}
