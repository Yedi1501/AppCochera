using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace AppCochera.Conexiones
{
    public class FirebaseService
    {
        public async Task AgregarUsuario(string correo, string nombre, string placa, int telefono)
        {
            await Constantes.firebase
                .Child("datosFormu")
                .PostAsync(new datosFormu
                {
                    Correo = correo,
                    Nombre = nombre,
                    Placa = placa,
                    Telefono = telefono,
                });
        }
    }

    public class datosFormu
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Placa { get; set; }
        public int Telefono { get; set; }
    }
}
