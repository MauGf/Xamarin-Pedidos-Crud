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

        //Constructor que me permite crear o conectarme a la BD
        //Crear la estructura de la tabla en base al modelo
        public SQLiteHelper(string dbpath)
        {
            db = new SQLiteAsyncConnection(dbpath);
            db.CreateTableAsync<Productos>().Wait();

        }

        //Metodo Asyncrono para actualizar y insertar registros
        //en una BD

        public Task<int> SaveItemAsync(Productos productos)
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

        //Metodo Asyncrono para listar informacion de la Base de datos
        public Task<List<Productos>> GetItemsAsycn()
        {
            return db.Table<Productos>().ToListAsync();
        }

        //Consulta por Id
        public Task<Productos> GetItemAsync(int productid)
        {
            return db.Table<Productos>().Where(i => i.Id == productid).FirstOrDefaultAsync();
            //Transct-SQL -Lenguaje Estructurado para consultas
            //Select * from productos where id=productid;
        }

        public Task<int> DeleteItemAsync(Productos productos)
        {
            return db.DeleteAsync(productos);
        }

    }
}
