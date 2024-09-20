using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

namespace AppCochera.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BienvenidaPage : ContentPage
    {
        private FirebaseClient firebase = new FirebaseClient("https://bdestacionamiento-2cc2e-default-rtdb.firebaseio.com/");

        public BienvenidaPage()
        {
            InitializeComponent();
        }

        private async void OnAgregarUsuarioClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los campos
                var correo = CorreoEntry.Text;
                var nombre = NombreEntry.Text;
                var placa = PlacaEntry.Text;
                var telefono = TelefonoEntry.Text;

                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(placa) || string.IsNullOrEmpty(telefono))
                {
                    await DisplayAlert("Error", "Todos los campos deben ser llenados", "OK");
                    return;
                }

                // Guardar datos en Firebase
                var response = await firebase
                    .Child("Usuarios")
                    .PostAsync(new Usuario { Correo = correo, Nombre = nombre, Placa = placa, Telefono = telefono });

                // Verificar la respuesta
                if (response != null)
                {
                    // Navegar a la página FlyoutPage
                    Application.Current.MainPage = new AppFlyoutPage();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la información en Firebase", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                await DisplayAlert("Error", $"Ocurrió un error al guardar los datos: {ex.Message}", "OK");
            }
        }



    }

    public class Usuario
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Placa { get; set; }
        public string Telefono { get; set; }
    }
}
