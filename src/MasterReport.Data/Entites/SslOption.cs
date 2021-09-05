using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterReport.Data.Entites
{
    public class SslOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SslOptionsId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Option { get; set; }
    }
}