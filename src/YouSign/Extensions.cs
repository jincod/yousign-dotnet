using System.IO;
using Newtonsoft.Json;

namespace YouSign
{
    public static class Extensions
    {
        public static T DeserializeJsonFromStream<T>(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();
                return ser.Deserialize<T>(jsonReader);
            }
        }
    }
}
