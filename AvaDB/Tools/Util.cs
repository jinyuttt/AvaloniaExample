using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaDB.Tools
{
    internal class Util
    {
      static  JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        public static string JsonSerialize<T> (T obj)
        {
          
            string jsonString = JsonSerializer.Serialize(obj,options);
            return jsonString;
        }
        public static T JsonDeserialize<T>(string  json)
        {
            T obj = JsonSerializer.Deserialize<T>(json);
            return obj;
        }
        public static async Task JsonFileSerializeAsync<T>(T obj,string file)
        {
            string fileName = file;
            FileInfo fileInfo = new FileInfo(fileName);
            
            if(!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
            
            await using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, obj);
           
        }
        public static async Task<T?> JsonFileDeserialize<T>(string file)
        {
            string fileName = file;
            if(!File.Exists(file))
            {
                return default(T);
            }
            using FileStream openStream = File.OpenRead(fileName);
            return await JsonSerializer.DeserializeAsync<T>(openStream)!;
           
        }
    }
}
