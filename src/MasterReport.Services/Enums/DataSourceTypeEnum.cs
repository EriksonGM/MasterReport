using System.ComponentModel.DataAnnotations;

namespace MasterReport.Services.Enums
{
    public enum DataSourceTypeEnum
    {
        [Display(Name = "Microsoft SQL Server")]
        MSSQL = 1,
        MySQL,
        PostgreSQL
    }
}