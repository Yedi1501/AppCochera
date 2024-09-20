using AppCochera.Conexiones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCochera
{
    public partial class MainPage : ContentPage
    {
        FirebaseService firebaseService = new FirebaseService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAgregarUsuarioClicked(object sender, EventArgs e)
        {
            string correo = correoEntry.Text;
            string nombre = nombreEntry.Text;
            string placa = placaEntry.Text;
            int telefono = Int32.Parse(telefonoEntry.Text.Trim());

            await firebaseService.AgregarUsuario(correo, nombre, placa, telefono);

            await DisplayAlert("Éxito", "Usuario agregado a Firebase", "OK");
        }
    }
}
    