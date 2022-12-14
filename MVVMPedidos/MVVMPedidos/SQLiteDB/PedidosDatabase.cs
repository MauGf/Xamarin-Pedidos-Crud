using MVVMPedidos.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MVVMPedidos.SQLiteDB
{
    class PedidosDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public PedidosDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Products).Name))
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Products)).ConfigureAwait(false);

                initialized = true;
            }
        }

        public Task<List<Products>> getProducts()
        {
            return Database.Table<Products>().ToListAsync();
        }

        public Task<int> saveProduct(Products product)
        {
            if (product.ID == 0)
                return Database.InsertAsync(product);
            else
                return Database.UpdateAsync(product);
        }

        public Task<int> deleteProduct(Products product)
        {
            return Database.DeleteAsync(product);
        }
    }
}
