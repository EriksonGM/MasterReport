using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }

        public Guid ReportId { get; set; }

        [ForeignKey("ReportId")]
        public Report Report { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Query { get; set; }

        public int DataSourceId { get; set; }

        [ForeignKey("DataSourceId")]
        public DataSource DataSource { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}