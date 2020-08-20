using System;
using SQLite;
using SmartWMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<List<User>> GetAllUsers()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> ReadUser(int id)
        {
            return _database.Table<User>().Where(p => p.UserId == id).FirstOrDefaultAsync();
        }

        public Response Login(string username, string password)
        {
            Response response = new Response();
            var user = _database.Table<User>().Where(p => p.Username == username).FirstOrDefaultAsync().Result;
            if(user == null)
            {
                response.Success = false;
                response.ExceptionMessage = "Bu isimde bir kullanıcı bulunmamaktadır!";
            }
            else if(user.Password == password)
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

        public Task<int> SaveUserAsyn(User user)
        {
            if (user.UserId != 0)
                return _database.UpdateAsync(user);
            else
                return _database.InsertAsync(user);
        }

    }
}
