using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace AppCochera.Conexiones
{
    public class Constantes
    {
        public static FirebaseClient firebase = new FirebaseClient("https://bdestacionamiento-2cc2e-default-rtdb.firebaseio.com/");
    }
}
