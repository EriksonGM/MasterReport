using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class ExecutionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionHistoryId { get; set; }
        
        public Guid ReportId { get; set; }

        [ForeignKey("ReportId")]
        public Report Report { get; set; }

        public int ExecutionTypeId { get; set; }

        [ForeignKey("ExecutionTypeId")]
        public ExecutionType ExecutionType { get; set; }

        public DateTime EventDate { get; set; } = DateTime.Now;
        
        [MaxLength(1000)]
        [Required]
        public string Obs { get; set; }

        public bool Succeeded { get; set; }
    }
}