using System;

namespace MasterReport.Services.Exceptions.Base
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {
            
        }
    }
}