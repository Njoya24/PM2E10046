
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E10046.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaSitios : ContentPage
    {
        public ListaSitios()
        {
            InitializeComponent();
            Title = "Lista";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaSitios();
            listaSitios.ItemsSource = lista;
        }


        }
  }


