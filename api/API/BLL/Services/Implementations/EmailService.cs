using API.BLL.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace API.BLL.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private string _smtpServer;
    private int _smtpPort;
    private string _smtpSender;
    private string _smtpPassword;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        InitializeData();
    }

    private void InitializeData()
    {
        var smtpConfig = _configuration.GetSection("SMTP");
        _smtpServer = smtpConfig.GetValue<string>("Server")!;
        _smtpPort = smtpConfig.GetValue<int>("Port");
        _smtpSender = smtpConfig.GetValue<string>("Sender")!;
        _smtpPassword = smtpConfig.GetValue<string>("Password")!;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = CreateEmailMessage(to, subject, body);
        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpSender, _smtpPassword);
            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }

    private MimeMessage CreateEmailMessage(string to, string subject, string body)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(_smtpSender));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("plain") { Text = body };

        return email;
    }
}
