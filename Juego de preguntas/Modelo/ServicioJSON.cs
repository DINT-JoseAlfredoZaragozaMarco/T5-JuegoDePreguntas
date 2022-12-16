using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_de_preguntas.Modelo
{
    class ServicioJSON
    {
        public void GuardarPreguntasEnJSON(ObservableCollection<Preguntas> preguntas, string ruta)
        {
            string personasJson = JsonConvert.SerializeObject(preguntas);
            File.WriteAllText(ruta, personasJson);
        }

        public ObservableCollection<Preguntas> CargarPreguntasDeJSON(string ruta)
        {
            ObservableCollection<Preguntas> preguntas = new ObservableCollection<Preguntas>();

             string personasJson = File.ReadAllText(ruta);
            preguntas = JsonConvert.DeserializeObject<ObservableCollection<Preguntas>>(personasJson);

            return preguntas;
        }
    }
}
