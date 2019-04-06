using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Wpf.Util
{
    public class StringService
    {
        private const string SVC_URL = "https://localhost:5001/api/values/";
        private const string GET_COUNT_URL = SVC_URL + "count";

        public IReadOnlyList<int> GetIndicies()
        {
            return GetSyncWebRequest(GET_COUNT_URL, x => Enumerable.Range(0, int.Parse(x)).ToList());
        }

        public string GetString(int idx)
        {
            return GetSyncWebRequest(SVC_URL + idx, x => FormatItem(idx, x));
        }

        private string FormatItem(int idx, string item) => $"{idx} - {item.Trim('"')}";

        private T GetSyncWebRequest<T>(string url, Func<string, T> formatter)
        {
            var req = WebRequest.CreateHttp(url);
            var resp = req.GetResponse();

            using (var stream = resp.GetResponseStream())
            using (var rdr = new StreamReader(stream))
            {
                var content = rdr.ReadToEnd();
                return formatter.Invoke(content);
            }
        }

        public async Task<IReadOnlyList<int>> GetIndiciesAsync()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(GET_COUNT_URL).ConfigureAwait(false);
                var count = int.Parse(result);
                return Enumerable.Range(0, count).ToList();
            }
        }

        public async Task<string> GetStringAsync(int idx)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(SVC_URL + idx).ConfigureAwait(false);
                return FormatItem(idx, result);
            }
        }
    }
}
