using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using AppFirebaseDb.Models;
using Newtonsoft.Json;

namespace AppFirebaseDb.Controlador
{
    public class ConexionFirebase
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://app-xamarin-9c42a-default-rtdb.firebaseio.com/");
        public async Task<bool> Save(Productos producto)
        {
            var data = await firebaseClient.Child(nameof(Productos)).PostAsync(JsonConvert.SerializeObject(producto));
            return true;
        }
    }
}
