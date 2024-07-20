using Newtonsoft.Json;
using System.Text.Json;

namespace DotNet8JsonCrud.Api.Helpers
{
    public class JsonFileHelper
    {
        private readonly string _filePath;

        public JsonFileHelper()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Blog.json");
        }

        public async Task<List<T>> GetJsonData<T>()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync(_filePath);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr)!;

            return lst;
        }
    }
}
