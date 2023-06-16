using PM02E10304.Controlador;
using PM02E10304.Page;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E10304
{
    public partial class App : Application
    {
        static BDSite basedatos;
        public static BDSite BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new BDSite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Examen.db3"));
                }
                return basedatos;
            }

        }

        public App()
        {
            InitializeComponent();


            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new AgregarSitio());
            }

        }

        protected override void OnStart()
        {
            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new AgregarSitio());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
