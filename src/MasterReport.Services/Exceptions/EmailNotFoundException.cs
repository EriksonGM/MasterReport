using MasterReport.Services.Exceptions.Base;

namespace MasterReport.Services.Exceptions
{
    public class EmailNotFoundException : NotFoundException
    {
        public EmailNotFoundException() : base("Email not found")
        {
        }
    }
}