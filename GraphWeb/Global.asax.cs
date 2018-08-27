using System;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

using log4net;
using log4net.Config;

namespace GraphWeb
{
    public class WebApiApplication : HttpApplication
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(WebApiApplication));

        public static NodeFactory Factory { get; set; }

        protected void Application_Start()
        {
            try
            {
                XmlConfigurator.Configure();

                if (_Log.IsInfoEnabled)
                    _Log.Info("Starting Global.asax");

                GlobalConfiguration.Configure(WebApiConfig.Register);
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));

                Factory = new NodeFactory(1000);
            }
            catch (Exception ex)
            {
                if (_Log.IsErrorEnabled)
                    _Log.Error(ex);
            }
        }

        protected void Session_Start(object sender, EventArgs e) { }

        protected void Application_BeginRequest(object sender, EventArgs e) { }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (_Log.IsFatalEnabled)
                _Log.Fatal("Unhandled application error caught", ex);

            if (ex is HttpException)
                Server.Transfer(Server.MapPath("Error.html"));
        }

        protected void Session_End(object sender, EventArgs e) { }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_Log.IsInfoEnabled)
                _Log.Info("Stopping Global.asax");
        }
    }
}
