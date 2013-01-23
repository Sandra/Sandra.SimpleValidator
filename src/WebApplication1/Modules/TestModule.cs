using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace WebApplication1.Modules
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/"] = _ =>
            {
                throw new AwesomeException("This is logged in a www.nancyfx.org application!");
            };
        }
    }

    public class AwesomeException : Exception
    {
        public AwesomeException(string message) : base(message)
        {
        }
    }
}