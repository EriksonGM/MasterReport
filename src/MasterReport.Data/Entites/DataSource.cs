using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class DataSource
    {
        [Key]
        public Guid DataSourceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int DataSourceTypeId { get; set; }

        [ForeignKey("DataSourceTypeId")]
        public DataSourceType DataSourceType { get; set; }

        [MaxLength(8000)]
        public string ConnectionString { get; set; }
    }
}