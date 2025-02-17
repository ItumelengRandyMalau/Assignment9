using SQLite;
using Assignment9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment9.DB_Services
{
    public class DatabaseServices
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseServices(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);
                // Add logging to confirm dbPath
                Console.WriteLine($"Database path: {dbPath}");

                // Create tables
                _database.CreateTableAsync<Profile>().Wait();
                _database.CreateTableAsync<ShoppingItem>().Wait();
                _database.CreateTableAsync<ShoppingCart>().Wait();
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                Console.WriteLine($"Database initialization error: {ex.Message}");
                throw;
            }
        }

        // Profile Methods
        public Task<int> SaveProfileAsync(Profile profile)
        {
            return _database.InsertOrReplaceAsync(profile);
        }

        public Task<Profile> GetProfileAsync()
        {
            return _database.Table<Profile>().FirstOrDefaultAsync();
        }

        // Shopping Items Methods
        public Task<List<ShoppingItem>> GetShoppingItemsAsync()
        {
            return _database.Table<ShoppingItem>().ToListAsync();
        }

        public Task<int> SaveShoppingItemAsync(ShoppingItem item)
        {
            return _database.InsertAsync(item);
        }

        // Shopping Cart Methods
        public Task<List<ShoppingCart>> GetShoppingCartAsync(int profileId)
        {
            return _database.Table<ShoppingCart>().Where(c => c.ProfileId == profileId).ToListAsync();
        }

        public Task<int> AddToCartAsync(ShoppingCart cartItem)
        {
            return _database.InsertAsync(cartItem);
        }

        public Task<int> RemoveFromCartAsync(ShoppingCart cartItem)
        {
            return _database.DeleteAsync(cartItem);


        }

        public async Task<ShoppingItem> GetShoppingItemAsync(int itemId)
        {
            return await _database.Table<ShoppingItem>().Where(i => i.Id == itemId).FirstOrDefaultAsync();
        }

        public async Task<ShoppingCart> GetCartItemAsync(int profileId, int itemId)
        {
            return await _database.Table<ShoppingCart>()
                                 .Where(c => c.ProfileId == profileId && c.ItemId == itemId)
                                 .FirstOrDefaultAsync();
        }

    }
}
