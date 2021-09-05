using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class DataSourceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DataSourceTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DataSourceName { get; set; }
    }
}