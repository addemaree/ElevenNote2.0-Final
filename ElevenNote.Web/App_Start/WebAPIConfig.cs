using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace ElevenNote.Web.App_Start
{
    public class WebAPIConfig
    {
        public static void Register()
        {
            //BoilerPlate Code: Can be re-used!
            GlobalConfiguration
                .Configure(
                    x =>
                    {
                        x
                            .Formatters
                            .JsonFormatter
                            .SupportedMediaTypes
                            .Add(new MediaTypeHeaderValue("text/html"));

                        x.MapHttpAttributeRoutes();
                    }
                );
        }
    }
}