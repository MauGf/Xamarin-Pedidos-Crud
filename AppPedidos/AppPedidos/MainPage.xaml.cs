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
            //listar pedidos
            var pedidosList = await App.SQLiteDb.GetItemsAsync();
            lstproductos.ItemsSource = pedidosList;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            Productos productos = new Productos()
            {
                NameProduct = txtname.Text,
                CantProduct = txtcantidad.Text,
                PriceProduct = txtprecio.Text
            };

            await App.SQLiteDb.SaveItemsAsync(productos);
            await DisplayAlert("Success", productos.NameProduct, "Ok");
            //listar pedidos
            var pedidosList = await App.SQLiteDb.GetItemsAsync();
            lstproductos.ItemsSource = pedidosList;
            //Adicionar pedidos

            //await SaveIemsAsync(Productos);

        }
    }
}
