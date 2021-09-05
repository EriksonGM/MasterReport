﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MasterReport.Data;
using MasterReport.Data.Entites;
using MasterReport.Models;
using MasterReport.Services.Base;
using MasterReport.Services.Enums;
using MasterReport.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace MasterReport.Services
{
    public interface IEmailAccountService : IService
    {
        public Task<List<EmailAccountDTO>> GetAll();

        public Task<List<SslOptionDTO>> GetSslOptions();

        public Task NewAccount(EmailAccountDTO dto);

        public Task UpdateAccount(EmailAccountDTO dto);

        public Task SendMail(MimeMessage msg, Guid emailAccountId);
        
        public Task TestMail(EmailAccountDTO account, string destiny);
    }

    public class EmailAccountService : Service, IEmailAccountService
    {
        public EmailAccountService(DataContext db) : base(db)
        {
        }
        
        public async Task<List<EmailAccountDTO>> GetAll()
        {
            return await _db.EmailAccounts
                .Include(x=>x.SslOption)
                .Select(x => new EmailAccountDTO(x))
                .ToListAsync();
        }

        public async Task<List<SslOptionDTO>> GetSslOptions()
        {
            return await _db.SslOptions
                .OrderBy(x => x.SslOptionsId)
                .Select(x => new SslOptionDTO(x))
                .ToListAsync();
        }

        public async Task NewAccount(EmailAccountDTO dto)
        {
            var eAccount = new EmailAccount
            {
                EmailAccountId = Guid.NewGuid(),
                SmtpServer = dto.SmtpServer,
                User = dto.User,
                Password = dto.Password,
                Port = dto.Port
            };

            await _db.EmailAccounts.AddAsync(eAccount);
        }

        public async Task UpdateAccount(EmailAccountDTO dto)
        {
            var account = await _db.EmailAccounts.FindAsync(dto.EmailAccountId);

            if (account == null)
                throw new EmailNotFoundException();

            account.SmtpServer = dto.SmtpServer;
            account.User = dto.User;
            account.Password = dto.Password;
            account.Port = dto.Port;
            account.SslOptionId = dto.SslOptionId;

            _db.Update(account);
        }

        public async Task SendMail(MimeMessage msg, Guid emailAccountId)
        {
            var account = await _db.EmailAccounts.FindAsync(emailAccountId);

            await SendMail(msg, account.SmtpServer, account.User, account.Password, account.Port,
                ConvertOptions((SslOptionEnum) account.SslOptionId));
        }
        
        public async Task TestMail(EmailAccountDTO account, string destiny)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress(Encoding.UTF8, destiny, destiny));

            await SendMail(msg, account.SmtpServer, account.User, account.Password, account.Port,
                ConvertOptions((SslOptionEnum)account.SslOptionId));
        }

        private async Task SendMail(MimeMessage msg, string smtpServer, string smtpUser, string smtpPass, int smtpPort, SecureSocketOptions sslOption)
        {
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(smtpServer, smtpPort, sslOption);

            await smtp.AuthenticateAsync(smtpUser, smtpPass);

            await smtp.SendAsync(msg);

            await smtp.DisconnectAsync(true);
        }

        private static SecureSocketOptions ConvertOptions(SslOptionEnum o)
        {
            return o switch
            {
                SslOptionEnum.None => SecureSocketOptions.None,
                SslOptionEnum.Auto => SecureSocketOptions.Auto,
                SslOptionEnum.SslOnConnect => SecureSocketOptions.SslOnConnect,
                SslOptionEnum.StartTls => SecureSocketOptions.StartTls,
                SslOptionEnum.StartTlsWhenAvailable => SecureSocketOptions.StartTlsWhenAvailable,
                _ => throw new ArgumentOutOfRangeException(nameof(o), o, null),
            };
        }
    }
}