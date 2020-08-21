using System.Collections.Generic;
using System.Threading.Tasks;
using SmartWMS.Models;
using SQLite;

namespace SmartWMS.Repository
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> ReadUserAsync(int id)
        {
            return _database.Table<User>().Where(p => p.UserId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.UserId != 0)
                return _database.UpdateAsync(user);
            else
                return _database.InsertAsync(user);
        }

        public Task<int>DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public Response LoginAsync(string username, string password)
        {
            Response response = new Response();
            var user = _database.Table<User>().Where(p => p.Username == username).FirstOrDefaultAsync().Result;
            if (user == null)
            {
                response.Success = false;
                response.ExceptionMessage = "Bu isimde bir kullanıcı bulunmamaktadır!";
            }
            else if (user.Password == password)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.ExceptionMessage = "Yanlış parola girdiniz!";
            }

            return response;

        }
    }
}