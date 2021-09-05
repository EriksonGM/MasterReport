using System.ComponentModel.DataAnnotations;

namespace MasterReport.Data.Entites
{
    public class ExecutionType
    {
        [Key]
        public int ExecutionTypeId { get; set; }

        [Required]
        [MaxLength(25)]
        public string TypeName { get; set; }
    }
}