using Data.Entitites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Tests.Helpers
{
    public class JsonDatabaseHelper<T> where T : BaseEntity
    {
        public static async Task<IEnumerable<T>> GetItems(string path)
        {
            try
            {
                using FileStream stream = File.OpenRead(path);

                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
