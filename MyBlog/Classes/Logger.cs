using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Classes
{
    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure(new Uri(@"C:\Users\Andrew\documents\visual studio 2010\Projects\MyBlog\MyBlog\Web.config"));
            log.Info("event occurred");
        }

    }
}
