using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPedidos.Models
{
    public class Productos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string CantProduct { get; set; }
        public string PriceProduct { get; set; }
    }
}
