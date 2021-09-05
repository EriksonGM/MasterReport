using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class EmailAccount
    {
        [Key]
        public Guid EmailAccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SmtpServer { get; set; }

        [Required]
        [MaxLength(50)]
        public string User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        
        public int Port { get; set; }

        public int SslOptionId { get; set; }

        [ForeignKey("SslOptionId")]
        public SslOption SslOption { get; set; }
    }
}