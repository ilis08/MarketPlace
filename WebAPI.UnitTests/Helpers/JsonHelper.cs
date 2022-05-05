using Data.Entitites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.Helpers
{
    public class JsonHelper
    {
        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public static async Task<IEnumerable<T>> GetItems<T>(string path) where T : BaseEntity
        {
            try
            {
                using FileStream stream = File.OpenRead(path);

                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, options);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
