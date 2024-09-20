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
    public partial class ParqueoPage : ContentPage
    {
        private FirebaseClient firebase;

        public ParqueoPage()
        {
            InitializeComponent();
            firebase = new FirebaseClient("https://appcochera-default-rtdb.firebaseio.com/");
            LoadParqueoData();
        }

        private async void LoadParqueoData()
        {
            try
            {
                var parqueos = await firebase
                    .Child("ActivoPasivo")
                    .OnceAsync<Parqueo>();

                // Crear una lista de objetos Parqueo para el ItemsSource
                var parqueoList = parqueos.Select(item => item.Object).ToList();

                // Imprimir los datos en la consola para depuración
                foreach (var parqueo in parqueoList)
                {
                    Console.WriteLine($"Correo: {parqueo.Correo}, Nombre: {parqueo.Nombre}, Placa: {parqueo.Placa}, Telefono: {parqueo.Telefono}, HoraInicio: {parqueo.HoraInicio}, ValorParqueo: {parqueo.ValorParqueo}");
                }

                // Forzar actualización del ListView
                ParqueoListView.ItemsSource = parqueoList;
                ParqueoListView.ItemsSource = null; // Forzar actualización
                ParqueoListView.ItemsSource = parqueoList;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al cargar los datos: {ex.Message}", "OK");
            }
        }
  
    }

    public class Parqueo
    {
        public string Correo { get; set; }
        public string HoraInicio { get; set; }
        public string Nombre { get; set; }
        public string Placa { get; set; }
        public string Telefono { get; set; }
        public string ValorParqueo { get; set; }
    }
}
