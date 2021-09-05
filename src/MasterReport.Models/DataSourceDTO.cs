using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MasterReport.Data.Entites;

namespace MasterReport.Models
{
    public class DataSourceDTO
    {
        public DataSourceDTO()
        {
            
        }

        public DataSourceDTO(DataSource d)
        {
            DataSourceId = d.DataSourceId;
            Name = d.Name;
            DataSourceTypeId = d.DataSourceTypeId;
            ConnectionString = d.ConnectionString;

            if (d.DataSourceType != null)
                DataSourceName = d.DataSourceType.DataSourceName;
        }
        public Guid? DataSourceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Data Source Type")]
        public int DataSourceTypeId { get; set; }
        
        public string DataSourceName { get; set; }

        [Required]
        [MaxLength(8000)]
        [Display(Name = "Connection String")]
        public string ConnectionString { get; set; }
        
    }
}