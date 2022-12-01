using CommunityToolkit.Mvvm.ComponentModel;
using Juego_de_preguntas.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_de_preguntas.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ObservableCollection<Preguntas> preguntas;

        private Preguntas preguntaActual;

        public Preguntas PreguntaActual
        {
            get { return preguntaActual; }
            set { SetProperty(ref preguntaActual, value); }
        }

        public MainWindowVM() 
        {
            
        }
    }
}
