using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Wpf.Util
{
    public class StringService
    {
        private readonly Random rnd = new Random();

        private Task Delay() => Task.Delay(rnd.Next(100, 500));

        public async Task<IEnumerable<int>> GetTop100()
        {
            await Delay().ConfigureAwait(false);
            return Enumerable.Range(1, 100);
        }

        public async Task<string> GetString(int id)
        {
            await Delay().ConfigureAwait(false);
            return string.Join(string.Empty, Enumerable.Repeat(id.ToString(), 10));
        }
    }
}
