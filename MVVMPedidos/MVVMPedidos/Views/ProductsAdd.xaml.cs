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
    public partial class ProductsAdd : ContentPage
    {
        public ProductsAdd(Products product)
        {
            try
            {
                InitializeComponent();
                ViewModelAddProduct vm = new ViewModelAddProduct(product);
                this.BindingContext = vm;
            }
            catch (Exception ex)
            {

            }

        }
    }
}