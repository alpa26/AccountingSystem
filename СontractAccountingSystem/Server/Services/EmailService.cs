using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using СontractAccountingSystem.Server.Services.Interfaces;
using СontractAccountingSystem.Server.Settings;

namespace СontractAccountingSystem.Server.Services;

public class EmailService : IEmailService
{
    private readonly IOptions<SmtpSetting> _smtpSetting;

    public EmailService(IOptions<SmtpSetting> smtpSetting)
    {
        _smtpSetting = smtpSetting;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Администрация", _smtpSetting.Value.UserEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using (var client = new SmtpClient())
        {
            try { 
            await client.ConnectAsync(_smtpSetting.Value.Host, _smtpSetting.Value.Port, false);
            await client.AuthenticateAsync(_smtpSetting.Value.UserEmail, _smtpSetting.Value.Password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
            } catch (Exception ex) { }
        }
    }
}
