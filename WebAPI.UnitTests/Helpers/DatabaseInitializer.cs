using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.Helpers
{
    public class DatabaseInitializer
    {
        private static readonly string path = @"../../../DatabaseMoq\";
        public static async Task InitializeAsync(RepositoryContext context)
        {
            await context.Categories.AddRangeAsync(await GetCategories(path + "categories.json"));
        }

        private static async Task<IEnumerable<Category>> GetCategories(string path)
        {
            try
            {
                var categories = await JsonHelper.GetItems<Category>(path);
                return categories;
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }
    }
}
