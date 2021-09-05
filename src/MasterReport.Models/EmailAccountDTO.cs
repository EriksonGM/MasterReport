using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using MasterReport.Data.Entites;

namespace MasterReport.Models
{
    public class EmailAccountDTO
    {
        public EmailAccountDTO(EmailAccount e)
        {
            EmailAccountId = e.EmailAccountId;
            SmtpServer = e.SmtpServer;
            User = e.User;
            Password = e.Password;
            Port = e.Port;
            SslOptionId = e.SslOptionId;

            if (e.SslOption != null)
                SslOption = e.SslOption.Option;
        }

        public Guid? EmailAccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SmtpServer { get; set; }

        [Required]
        [MaxLength(50)]
        public string User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public int Port { get; set; }

        public int SslOptionId { get; set; }

        public string SslOption { get; set; }
    }
}
