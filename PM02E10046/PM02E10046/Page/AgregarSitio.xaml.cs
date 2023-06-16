using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E10304.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarSitio : ContentPage
    {
        string base64Val = "";
        public AgregarSitio()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Datos_Ubicacion();
        }

        private void btncargarimg_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnlista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ListaSitios()));
        }

        private async void btncargarimg_Clicked_1(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64Val = Convert.ToBase64String(imagenByte);
                //await EmpleController.SubirImagen(imagenByte);
            }


        }

        public async void Datos_Ubicacion()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                    lbllatitud.Text = "00.0";
                    lbllongitud.Text = "00.0";
                }
                if (location != null)
                {
                    lbllatitud.Text = location.Latitude.ToString();
                    lbllongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
            }
            catch (FeatureNotEnabledException)
            {
            }
            catch (PermissionException)
            {
            }
            catch (Exception)
            {
            }
        }

        public async void Guardar_Datos()
        {
            var casas = new Entidades.Sitios
            {
                latitud = Convert.ToDouble(lbllatitud.Text),
                longitud = Convert.ToDouble(lbllatitud.Text),
                descripcion = describir.Text,
                image = base64Val
            };

            var resultado = await App.BaseDatos.GrabarSitios(casas);

            if (resultado == 1)
            {
                await DisplayAlert("Mensaje", "Registro exitoso!!!", "ok");
                clear();
                Datos_Ubicacion();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Guardar", "ok");
            }
        }

        public void clear()
        {
            lbllatitud.Text = lbllongitud.Text = describir.Text = base64Val = String.Empty;
        }

        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(describir.Text) == true)
                        {
                            await DisplayAlert("Error", "Debe describir la direccion del sitio", "Ok");
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(base64Val) == true)
                            {
                                await DisplayAlert("Error", "Ingrese la foto", "Ok");
                            }
                            else
                            {
                                Guardar_Datos();
                            }

                        }

                }
               
            }
            catch (FeatureNotSupportedException)
            {
            }
            catch (FeatureNotEnabledException)
            {
            }
            catch (PermissionException)
            {
            }
            catch (Exception)
            {
            }
        }
    }
}