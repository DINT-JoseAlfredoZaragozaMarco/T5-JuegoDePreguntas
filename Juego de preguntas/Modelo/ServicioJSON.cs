using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Juego_de_preguntas.Modelo
{
    class ServicioJSON
    {
        public void GuardarPreguntasEnJSON(ObservableCollection<Preguntas> preguntas, string ruta)
        {
            if(ruta != null)
            {
                string personasJson = JsonConvert.SerializeObject(preguntas);
                File.WriteAllText(ruta, personasJson);
                MessageBox.Show("Preguntas guardadas correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        public ObservableCollection<Preguntas> CargarPreguntasDeJSON(string ruta)
        {
            ObservableCollection<Preguntas> preguntas = new ObservableCollection<Preguntas>();

            if (ruta != null)
            {
                string personasJson = File.ReadAllText(ruta);
                preguntas = JsonConvert.DeserializeObject<ObservableCollection<Preguntas>>(personasJson);
                MessageBox.Show("Preguntas cargadas correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return preguntas;
            }
            else
            {
                return null;
            }
        }
    }
}
