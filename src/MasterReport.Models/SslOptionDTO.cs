using MasterReport.Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace MasterReport.Models
{
    public class SslOptionDTO
    {
        public SslOptionDTO(SslOption s)
        {
            SslOptionsId = s.SslOptionsId;
            Option = s.Option;
        }

        public int SslOptionsId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Option { get; set; }
    }
}