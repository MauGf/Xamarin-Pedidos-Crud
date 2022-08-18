using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppPedidos.Controller;
using SQLite;
using AppPedidos.Models;

namespace AppPedidos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //Listar Pedidos
            var pedidosList = await App.SQLiteDb.GetItemsAsycn();
            lstproductos.ItemsSource = pedidosList;
        }



        private async void btnAdd_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtnombre.Text))
            {

                Productos productos = new Productos()
                {
                    NameProduct = txtnombre.Text,
                    CantProduct = Convert.ToInt32(txtcantidad.Text),
                    PriceProduct = Convert.ToDecimal(txtprecio.Text)
                };
                //Add Pedidos
                await App.SQLiteDb.SaveItemAsync(productos);
                txtnombre.Text = string.Empty;
                txtcantidad.Text = string.Empty;
                txtprecio.Text = string.Empty;
                await DisplayAlert("Success", productos.NameProduct, "Ok");
                //Listar Pedidos
                var pedidosList = await App.SQLiteDb.GetItemsAsycn();
                lstproductos.ItemsSource = pedidosList;
            }

            else
            {
                await DisplayAlert("Required", "No Puede Dejar Vacio el nombre", "Ok");
            }

        }



        private async void btnRead_Clicked(object sender, EventArgs e)
        {
            //Get Pedido
            var product = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtid.Text));
            txtnombre.Text = product.NameProduct;
            txtcantidad.Text = Convert.ToString(product.CantProduct);
            txtprecio.Text = Convert.ToString(product.PriceProduct);

        }

        private async void btnupdate_Clicked(object sender, EventArgs e)
        {
            Productos productos = new Productos()
            {
                Id = Convert.ToInt32(txtid.Text),
                NameProduct = txtnombre.Text,
                CantProduct = Convert.ToInt32(txtcantidad.Text),
                PriceProduct = Convert.ToDecimal(txtprecio.Text)
            };
            //Actualizar el Pedido
            await App.SQLiteDb.SaveItemAsync(productos);
            txtid.Text = String.Empty;
            txtnombre.Text = string.Empty;
            txtcantidad.Text = string.Empty;
            txtprecio.Text = string.Empty;
            //Listar Pedidos
            var pedidosList = await App.SQLiteDb.GetItemsAsycn();
            lstproductos.ItemsSource = pedidosList;
        }


        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            var product = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtid.Text));
            await App.SQLiteDb.DeleteItemAsync(product);
            txtnombre.Text = string.Empty;
            txtcantidad.Text = string.Empty;
            txtprecio.Text = string.Empty;
            //Listar Pedidos
            var pedidosList = await App.SQLiteDb.GetItemsAsycn();
            lstproductos.ItemsSource = pedidosList;
            await DisplayAlert("Success", "Se elimino el Producto Correctamente", "OK");

        }
    }

}
