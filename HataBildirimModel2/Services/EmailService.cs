using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace HataBildirimModel2.Services
{
    public class EmailService
    {
        public async Task<bool> SendVerificationCodeAsync(string email, string verificationCode)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Hata Bildirim Sistemi", "hatabildirimsistemi@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Şifre Sıfırlama Doğrulama Kodu";

                message.Body = new TextPart("plain") { Text = $"Doğrulama kodunuz: {verificationCode}" };

                using var client = new SmtpClient();

                // FMail özel ayarı
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync("smtp.fmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("hatabildirimsistemi@gmail.com", "13579admin");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Hata loglayın
                Console.WriteLine($"Mail gönderim hatası: {ex.ToString()}");
                return false;
            }
        }
    }
}