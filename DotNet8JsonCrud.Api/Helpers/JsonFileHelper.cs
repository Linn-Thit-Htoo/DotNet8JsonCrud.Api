namespace DotNet8JsonCrud.Api.Helpers;

public class JsonFileHelper
{
    private readonly string _filePath;

    public JsonFileHelper()
    {
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Blog.json");
    }

    #region Get Json Data

    public async Task<List<T>> GetJsonData<T>()
    {
        try
        {
            string jsonStr = await ReadFile();
            var lst = jsonStr.Deserialize<List<T>>()!;

            return lst;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Write Json Data V1

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

    #endregion

    #region Write Json Data

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

    #endregion

    private async Task<string> ReadFile() =>
        await File.ReadAllTextAsync(_filePath);
}
