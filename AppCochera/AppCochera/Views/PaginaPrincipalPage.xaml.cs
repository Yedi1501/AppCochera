using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCochera.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipalPage : ContentPage
    {
        public PaginaPrincipalPage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnIrAParqueoTapped;
            //ParqueoCard.GestureRecognizers.Add(tapGestureRecognizer);
            CrearCuadrosDinamicos();
            CargarDatosDesdeFirebase();
        }

        private async Task CargarDatosDesdeFirebase()
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://bdestacionamiento-2cc2e-default-rtdb.firebaseio.com/");

            try
            {
                var data = await firebaseClient
                    .Child("/ActivoPasivo")
                    .OnceSingleAsync<Dictionary<string, Dictionary<string, object>>>();

                foreach (var item in data)
                {
                    var valores = item.Value;

                    string valorParqueo = valores.ContainsKey("valorParqueo") ? valores["valorParqueo"].ToString() : "N/A";
                    var frame = CardsGrid.Children
                        .OfType<Frame>()
                        .FirstOrDefault(f => f.StyleId == valorParqueo);

                    if (frame != null)
                    {
                        frame.BackgroundColor = Color.Orange;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private void CrearCuadrosDinamicos()
        {
            int totalCuadros = 32;
            int columnas = 4;

            for (int i = 0; i < totalCuadros; i++)
            {
                Label textoLabel = new Label
                {
                    Text = $"Parq {i + 1}",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14,
                    TextColor = Color.Black
                };

                Frame cuadro = new Frame
                {
                    CornerRadius = 10,
                    HasShadow = true,
                    Padding = 0,
                    BackgroundColor = Color.LightSkyBlue,
                    HeightRequest = 80,
                    WidthRequest = 80,
                    Content = textoLabel
                };
                string cuadroId = $"pas{i + 1}";
                cuadro.StyleId = cuadroId;
                CardsGrid.Children.Add(cuadro, i % columnas, i / columnas);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await DisplayAlert("Bienvenido", "Has llegado a la página principal", "OK");
        }

        private async void OnIrAParqueoTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ParqueoPage());
        }
    }
}
