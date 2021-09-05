using MasterReport.Services.Exceptions.Base;

namespace MasterReport.Services.Exceptions
{
    public class DataSourceNotFoundException : NotFoundException
    {
        public DataSourceNotFoundException() : base("DataSource not found")
        {
        }
    }
}