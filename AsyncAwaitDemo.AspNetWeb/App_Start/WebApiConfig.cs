using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AsyncAwaitDemo.AspNetWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.MapHttpAttributeRoutes();
        }
    }
}
