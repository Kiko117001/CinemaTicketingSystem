﻿using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Repository.Interface;
using CinemaTicketingSystem.Service.Interface;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketingSystem.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly IRepository<EmailMessage> _mailRepository;

        public EmailService(EmailSettings settings, IRepository<EmailMessage> mailRepository)
        {
            _settings = settings;
            _mailRepository = mailRepository;
        }

        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach (var item in allMails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                    Subject = item.Subject
                };

                emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = item.Content };

                emailMessage.To.Add(new MailboxAddress(item.MailTo, item.MailTo));

                messages.Add(emailMessage);

                item.Status = true;
                this._mailRepository.Update(item);
            }

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                        
                    }

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
