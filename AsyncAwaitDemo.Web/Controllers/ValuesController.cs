using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AsyncAwaitDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private Random rnd = new Random();

        // GET api/values
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCount()
        {
            await Task.Delay(rnd.Next(1000, 2000));
            var items = await GetItems();
            return new JsonResult(items.Count);
        }

        // GET api/values/5
        [HttpGet("{idx}")]
        public async Task<ActionResult<string>> Get(int idx)
        {
            await Task.Delay(rnd.Next(1000, 2000));
            var items = await GetItems().ConfigureAwait(false);
            return new JsonResult(items[idx]);
        }

        private async Task<IReadOnlyList<string>> GetItems()
        {
            using (var fs = System.IO.File.OpenRead("files\\data.csv"))
            using (var rdr = new StreamReader(fs))
            {
                var items = new List<string>();
                while (!rdr.EndOfStream)
                {
                    var x = await rdr.ReadLineAsync().ConfigureAwait(false);
                    items.Add(x);
                }

                return items;
            }
        }
    }
}
