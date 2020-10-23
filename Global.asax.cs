using NLog;
using System;
using System.Web;
using System.Web.Http;

namespace file_uploader
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc != null && exc is HttpUnhandledException) 
                logger.Error(exc, exc.Message);
        }
    }
}


