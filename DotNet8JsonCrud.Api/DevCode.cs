using Newtonsoft.Json;

namespace DotNet8JsonCrud.Api
{
    public static class DevCode
    {
        public static bool IsNullOrEmpty(this string str) =>
            string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);

        public static string Serialize(this object obj) =>
            JsonConvert.SerializeObject(obj);

        public static object Deserialize<T>(this string jsonStr) =>
            JsonConvert.DeserializeObject<T>(jsonStr)!;
    }
}
