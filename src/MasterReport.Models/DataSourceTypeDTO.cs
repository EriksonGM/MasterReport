using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterReport.Data.Entites;

namespace MasterReport.Models
{
    public class DataSourceTypeDTO
    {
        public DataSourceTypeDTO()
        {
            
        }

        public DataSourceTypeDTO(DataSourceType d)
        {
            DataSourceTypeId = d.DataSourceTypeId;
            DataSourceName = d.DataSourceName;
        }
        public int DataSourceTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DataSourceName { get; set; }
    }
}
