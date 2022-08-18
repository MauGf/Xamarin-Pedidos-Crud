using MVVMPedidos.Models;
using MVVMPedidos.SQLiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MVVMPedidos.ViewModels
{
    public class ViewModelAddProduct : INotifyPropertyChanged
    {
        private Products _products { get; set; }

        public Products product
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

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

        private string _btnSaveLabel { get; set; }
        public string btnSaveLabel
        {
            get { return _btnSaveLabel; }
            set
            {
                _btnSaveLabel = value;
                OnPropertyChanged();
            }
        }

        public Command btnSaveProduct { get; set; }
        public Command btnClearProduct { get; set; }
        public ViewModelAddProduct(Products obj)
        {
            if (obj == null || obj.ID == 0)
                ClearProduct();

            else
            {
                product = obj;
                btnSaveLabel = "UPDATE";
            }
            btnSaveProduct = new Command(SaveProduct);
            btnClearProduct = new Command(ClearProduct);
        }

        public void SaveProduct()
        {
            try
            {
                PedidosDatabase productDatabase = new PedidosDatabase();
                int i = productDatabase.saveProduct(product).Result;

                if (i == 1)
                {

                    if (btnSaveLabel.Equals("ADD"))
                    {
                        ClearProduct();
                        lblInfo = "Su producto se guardó con éxito.";
                    }
                    else
                    {
                        ClearProduct();
                        lblInfo = "Su producto se actualizó con éxito.";
                    }
                }
                else
                    lblInfo = "No se puede guardar la información del producto";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void ClearProduct()
        {
            product = new Products();
            product.ID = 0;
            product.Name = "";
            product.Price = null;
            product.Quantity = null;
            lblInfo = "";
            btnSaveLabel = "ADD";
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
