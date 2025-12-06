using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using RegistroDePaqueteEPS.Data;

public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        SendEmailAsync(email, "Confirma tu email", $"Por favor confirma tu cuenta haciendo <a href='{confirmationLink}'>clic aquí</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        SendEmailAsync(email, "Restablecer Contraseña", $"Restablece tu contraseña haciendo <a href='{resetLink}'>clic aquí</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        SendEmailAsync(email, "Código de restablecimiento", $"Tu código es: {resetCode}");

    // Lógica principal de envío SMTP
    private async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        // Asegúrate de tener estos valores en tu appsettings.json
        var mail = "tucorreo@gmail.com";
        var pw = "tu_contraseña_de_aplicacion";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };

        var mailMessage = new MailMessage(from: mail, to: toEmail, subject, message);
        mailMessage.IsBodyHtml = true;

        await client.SendMailAsync(mailMessage);
    }
}