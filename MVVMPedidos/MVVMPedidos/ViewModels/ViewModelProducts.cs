using MVVMPedidos.Models;
using MVVMPedidos.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MVVMPedidos.ViewModels
{
    public class ViewModelProducts: INotifyPropertyChanged
    {
        private ObservableCollection<Products> _lstProducts { get; set; }

        public ObservableCollection<Products> lstProducts
        {
            get { return _lstProducts; }
            set
            {
                _lstProducts = value;
                OnPropertyChanged();
            }
        }

        public Command btnAddProduct { get; set; }

        private string _lblInfo { get; set; }
        public string lblInfo
        {
            get { return _lblInfo; }
            set
            {
                _lblInfo = value;
                OnPropertyChanged();
            }
        }

        public ViewModelProducts()
        {
            lstProducts = new ObservableCollection<Products>();

        }

        public void GetProducts()
        {
            try
            {
                PedidosDatabase productDatabase = new PedidosDatabase();
                var products = productDatabase.getProducts().Result;

                if (products != null && products.Count > 0)
                {
                    lstProducts = new ObservableCollection<Products>();

                    foreach (var prod in products)
                    {
                        lstProducts.Add(new Products
                        {
                            ID = prod.ID,
                            Name = prod.Name,
                            Price = prod.Price,
                            Quantity = prod.Quantity
                        });
                    }

                    lblInfo = "Total " + products.Count.ToString() + " registros encontrados";
                }
                else
                    lblInfo = "No se encontraron registros de productos. por favor agregue uno";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void DeleteProduct(Products product)
        {
            try
            {
                PedidosDatabase productDatabase = new PedidosDatabase();
                var result = productDatabase.deleteProduct(product).Result;

                if (result == 1)
                    GetProducts();
                else
                    lblInfo = "No se puede eliminar este producto.";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
