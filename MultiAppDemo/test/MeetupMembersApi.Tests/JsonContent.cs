using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MeetupMembersApi.Tests
{
    public class JsonContent : StreamContent
    {
        private static readonly Encoding Encoding = new UTF8Encoding(false);

        public JsonContent(object obj) : this(obj, JsonSerializer.CreateDefault())
        {
        }

        public JsonContent(object obj, JsonSerializerSettings settings) : this(obj, JsonSerializer.Create(settings))
        {
        }

        private JsonContent(object obj, JsonSerializer serializer) : base(GetJsonStream(obj, serializer))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json")
            {
                CharSet = Encoding.UTF8.WebName
            };
        }

        private static Stream GetJsonStream(object obj, JsonSerializer serializer)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var ms = new MemoryStream();

            using (var sw = new StreamWriter(ms, Encoding, 1024, true))
            using (var jtw = new JsonTextWriter(sw))
                serializer.Serialize(jtw, obj);

            ms.Position = 0;

            return ms;
        }
    }
}