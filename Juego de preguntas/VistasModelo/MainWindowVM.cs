using CommunityToolkit.Mvvm.ComponentModel;
using Juego_de_preguntas.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Juego_de_preguntas.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ServicioAzureBlobStorage servicioAzure;

        private ObservableCollection<Preguntas> preguntas;
        public ObservableCollection<Preguntas> Preguntas
        {
            get { return preguntas; }
            set { SetProperty(ref preguntas, value); }
        }

        private Preguntas nuevaPregunta;
        public Preguntas NuevaPregunta
        {
            get { return nuevaPregunta; }
            set { SetProperty(ref nuevaPregunta, value); }
        }
        private ObservableCollection<String> dificultades;
        public ObservableCollection<String> Dificultades
        {
            get { return dificultades; }
            set { SetProperty(ref dificultades, value); }
        }

        private ObservableCollection<String> categorias;
        public ObservableCollection<String> Categorias
        {
            get { return categorias; }
            set { SetProperty(ref categorias, value); }
        }
        private Preguntas preguntaActual;

        public Preguntas PreguntaActual
        {
            get { return preguntaActual; }
            set { SetProperty(ref preguntaActual, value); }
        }

        public MainWindowVM() 
        {
            // Inicialización
            preguntas = new ObservableCollection<Preguntas>();
            servicioAzure = new ServicioAzureBlobStorage();
            dificultades = new ObservableCollection<String>();
            categorias = new ObservableCollection<String>();
            nuevaPregunta = new Preguntas();

            // Añadimos dificultades a la lista de dificultades
            dificultades.Add("Fácil");
            dificultades.Add("Medio");
            dificultades.Add("Difícil");
            dificultades.Add("Imposible");

            // Añadimos categorias a la lista de categorias
            categorias.Add("Clase-D");
            categorias.Add("SCP's");
            categorias.Add("Guardias de la instalación");
            categorias.Add("Científicos");
        }

        public void Examinar(TextBox textBoxURL)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                textBoxURL.Text = servicioAzure.GuardarImagenEnAzure(openFileDialog.FileName);
            }
        }

        public void AñadePregunta()
        {
            preguntas.Add(nuevaPregunta);
        }
    }
}
