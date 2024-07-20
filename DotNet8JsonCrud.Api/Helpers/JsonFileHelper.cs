using DotNet8JsonCrud.Api.Models;
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
            try
            {
                string jsonStr = await System.IO.File.ReadAllTextAsync(_filePath);
                List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr)!;

                return lst;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task WriteJsonData(BlogModel blog)
        {
            try
            {
                var existingData = await GetJsonData<BlogModel>();

                existingData.Add(blog);

                string jsonStr = JsonConvert.SerializeObject(existingData, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(_filePath, jsonStr);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
