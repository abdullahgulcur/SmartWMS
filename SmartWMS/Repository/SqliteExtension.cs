using SmartWMS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartWMS.Repository
{
    public static class SqliteExtension
    {
        public static SQLiteConnection GetConnection()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.CreateTable<StorageLocation>();
            connection.CreateTable<Item>();
            connection.CreateTable<Stock>();
            return connection;
        }
    }
}
