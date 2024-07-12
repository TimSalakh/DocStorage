using API.BLL.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace API.BLL.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpSender;
    private readonly string _smtpPassword;

    public EmailService(IConfiguration configuration)
    {
        _smtpServer = configuration.GetSection("SMTP:Server").Value!;
        _smtpPort = int.Parse(configuration.GetSection("SMTP:Port").Value!);
        _smtpSender = configuration.GetSection("SMTP:Sender").Value!;
        _smtpPassword = configuration.GetSection("SMTP:Password").Value!;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_smtpSender));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_smtpSender, _smtpPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    } 
}
