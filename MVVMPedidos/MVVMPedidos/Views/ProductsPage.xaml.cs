using MVVMPedidos.Models;
using MVVMPedidos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMPedidos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        ViewModelProducts vm;
        public ProductsPage()
        {
            InitializeComponent();
            vm = new ViewModelProducts();
            this.BindingContext = vm;
        }


        protected override void OnAppearing()
        {
            if (vm != null)
                vm.GetProducts();

            base.OnAppearing();
        }

        private async void btnAddProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsAdd(null));
        }

        private async void lstProduct_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (lstProduct.SelectedItem != null)
                {
                    var product = (Products)lstProduct.SelectedItem;
                    lstProduct.SelectedItem = null;
                    await Navigation.PushAsync(new ProductsAdd(product));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void btnDeleteProduct_Clicked(object sender, EventArgs e)
        {
            try
            {
                string ID = (sender as Button).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(ID))
                {
                    var product = vm.lstProducts.Where(x => x.ID.ToString() == ID);
                    var result = await DisplayAlert("Confirmar", "¿Desea eliminar Producto:" + product.FirstOrDefault().Name + "?", "Si", "No");
                    if (result)
                        vm.DeleteProduct(product.FirstOrDefault());
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}