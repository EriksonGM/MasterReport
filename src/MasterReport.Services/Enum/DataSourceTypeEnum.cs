using System.ComponentModel.DataAnnotations;

namespace MasterReport.Services.Enum
{
    public enum DataSourceTypeEnum
    {
        [Display(Name = "Microsoft SQL Server")]
        MSSQL = 1,
        MySQL,
        PostgreSQL
    }
}