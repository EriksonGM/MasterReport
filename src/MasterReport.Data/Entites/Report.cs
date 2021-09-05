using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ReportId { get; set; }

        public Guid EmailAccountId { get; set; }

        [ForeignKey("EmailAccountId")]
        public EmailAccount EmailAccount { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public bool Force { get; set; }

        public ICollection<ExecutionHistory> History { get; set; }

        public ICollection<Destiny> Destinies { get; set; }
    }
}