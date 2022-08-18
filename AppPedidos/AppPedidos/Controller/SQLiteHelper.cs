using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AppPedidos.Models;

namespace AppPedidos.Controller
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        //constructor que me permite crear la estructura de la tabla d ela base de datos al modelo
        public SQLiteHelper(string dbpath)
        {
            db = new SQLiteAsyncConnection(dbpath);
            db.CreateTableAsync<Productos>().Wait();

        }

        //Metoo asyncrono para actualizar e inserter registros en un abd
        public Task <int> SaveItemsAsync(Productos productos)
        {
            if (productos.Id != 0)
            {
                return db.UpdateAsync(productos);
            }
            else
            {
                return db.InsertAsync(productos);
            }
        }

        public Task<List<Productos>> GetItemsAsync()
        {
            return db.Table<Productos>().ToListAsync();
        }
    }
}
