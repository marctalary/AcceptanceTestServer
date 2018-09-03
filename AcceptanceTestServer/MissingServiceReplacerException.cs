using System;

namespace AcceptanceTestServer
{
    public class MissingServiceReplacerException : Exception
    {
        public MissingServiceReplacerException() :base("ServiceReplacer not found in service collection")
        {
            
        }
    }
}
