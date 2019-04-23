using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitDemo.AspNetWeb.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        private readonly static Random rnd = new Random();

        [HttpGet]
        [Route("count")]
        public async Task<IHttpActionResult> GetCount([FromUri]bool captureContext = true)
        {
            var values = await GetValues(captureContext).ConfigureAwait(captureContext);
            return Json(values.Count);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]bool captureContext = true)
        {
            var values = await GetValues(captureContext).ConfigureAwait(captureContext);
            return Json(values);
        }

        [HttpGet]
        [Route("{idx}")]
        public async Task<IHttpActionResult> Get(int idx, [FromUri]bool captureContext = true)
        {
            var values = await GetValues(captureContext).ConfigureAwait(captureContext);
            var selectedValue = values.Skip(idx).FirstOrDefault();
            return Json(selectedValue);
        }

        private async Task<IReadOnlyList<string>> GetValues(bool captureContext)
        {
            var path = HttpContext.Current.Server.MapPath("~/files/data.csv");

            using (var fs = File.OpenRead(path))
            using (var rdr = new StreamReader(fs))
            {
                var values = new List<string>();
                while (!rdr.EndOfStream)
                {
                    var x = await rdr.ReadLineAsync().ConfigureAwait(captureContext);
                    var encoded = HttpContext.Current.Server.UrlEncode(x);
                    values.Add(encoded);

                    await Task.Delay(rnd.Next(50, 100)).ConfigureAwait(captureContext);
                }

                return values;
            }
        }
    }
}
