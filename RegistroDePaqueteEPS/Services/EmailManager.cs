namespace RegistroDePaqueteEPS.Services;

public class EmailManager(IEmailService _emailService)
{
    private readonly string _adminEmail = "buandblaivaeps@gmail.com";

    public async Task SendUserAccountConfirmationAsync(string email, string code)
    {
        string subject = "Confirma tu cuenta - Código de verificación";
        string body = $"<h1>Hola,</h1><p>Tu código de verificación es: <strong>{code}</strong></p>";
        await _emailService.SendEmailAsync(email, subject, body);
    }

    public async Task SendUserPasswordRecoveryAsync(string email, string code)
    {
        string subject = "Recuperación de contraseña";
        string body = $"<p>Has solicitado restablecer tu contraseña.</p><p>Usa este código: <strong>{code}</strong></p>";
        await _emailService.SendEmailAsync(email, subject, body);
    }

    public async Task SendUserWelcomeAsync(string email, string userName, string numeroEps)
    {
        string subject = "¡Bienvenido a EPS Salcedo!";
        string body = $"<h1>Hola {userName},</h1><p>Gracias por unirte a nosotros. Tu numero de EPS es: {numeroEps}</p>";
        await _emailService.SendEmailAsync(email, subject, body);

        await SendAdminNewUserAlertAsync(userName, email);
    }

    public async Task SendUserAccountDeletedAsync(string email)
    {
        string subject = "Tu cuenta ha sido eliminada";
        string body = "<p>Lamentamos verte partir. Tu cuenta y datos han sido borrados correctamente.</p>";
        await _emailService.SendEmailAsync(email, subject, body);

        await SendAdminUserDeletedAlertAsync(email);
    }

    public async Task SendUserOrderReceivedAsync(string email, string orderId)
    {
        string subject = $"Pedido #{orderId} Recibido";
        string body = $"<p>Hemos recibido tu pedido #{orderId} y lo estamos procesando.</p>";
        await _emailService.SendEmailAsync(email, subject, body);
    }

    public async Task SendUserOrderReadyAsync(string email, string orderId)
    {
        string subject = $"¡Tu Pedido #{orderId} está listo!";
        string body = $"<p>Buenas noticias, tu pedido #{orderId} ya está disponible para retirar.</p>";
        await _emailService.SendEmailAsync(email, subject, body);
    }

    private async Task SendAdminNewUserAlertAsync(string userName, string userEmail)
    {
        string subject = "[ALERTA] Nuevo usuario registrado";
        string body = $"<p>Se ha registrado: {userName} ({userEmail})</p>";
        await _emailService.SendEmailAsync(_adminEmail, subject, body);
    }

    private async Task SendAdminUserDeletedAlertAsync(string userEmail)
    {
        string subject = "[ALERTA] Usuario eliminado";
        string body = $"<p>El usuario {userEmail} ha eliminado su cuenta.</p>";
        await _emailService.SendEmailAsync(_adminEmail, subject, body);
    }
}
