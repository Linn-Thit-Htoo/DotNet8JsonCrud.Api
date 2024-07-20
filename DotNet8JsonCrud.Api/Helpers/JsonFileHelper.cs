namespace DotNet8JsonCrud.Api.Helpers;

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
            string jsonStr = await File.ReadAllTextAsync(_filePath);
            var lst = jsonStr.Deserialize<List<T>>()!;

            return lst;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task WriteJsonDataV1<T>(List<T> lst)
    {
        try
        {
            string jsonStr = lst.Serialize();
            await File.WriteAllTextAsync(_filePath, jsonStr);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task WriteJsonData<T>(T model)
    {
        try
        {
            var lst = await GetJsonData<T>();
            lst.Add(model);

            string jsonStr = JsonConvert.SerializeObject(lst, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(_filePath, jsonStr);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
