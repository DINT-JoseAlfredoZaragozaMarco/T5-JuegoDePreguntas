using CommunityToolkit.Mvvm.ComponentModel;
using Juego_de_preguntas.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Juego_de_preguntas.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ServicioAzureBlobStorage servicioAzure;
        private ServicioJSON servicioJSON;
        private ServicioDialogos servicioDialogos;

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

        private ObservableCollection<Preguntas> preguntasPartida;

        public ObservableCollection<Preguntas> PreguntasPartida
        {
            get { return preguntasPartida; }
            set { SetProperty(ref preguntasPartida, value); }
        }

        private int dificultadSeleccionada;

        public int DificultadSeleccionada
        {
            get { return dificultadSeleccionada; }
            set { SetProperty(ref dificultadSeleccionada, value); }
        }

        private Preguntas preguntaAJugar;

        public Preguntas PreguntaAJugar
        {
            get { return preguntaAJugar; }
            set { SetProperty(ref preguntaAJugar, value); }
        }
        private int posicionPreguntasAJugar;
        private string respuestaIntroducida;

        public string RespuestaIntroducida
        {
            get { return respuestaIntroducida; }
            set { SetProperty(ref respuestaIntroducida, value); }
        }


        public MainWindowVM() 
        {
            // Inicialización
            preguntasPartida = new ObservableCollection<Preguntas>();
            servicioDialogos = new ServicioDialogos();
            servicioJSON = new ServicioJSON();
            preguntas = new ObservableCollection<Preguntas>();
            servicioAzure = new ServicioAzureBlobStorage();
            dificultades = new ObservableCollection<string>();
            categorias = new ObservableCollection<string>();
            nuevaPregunta = new Preguntas();
            preguntaAJugar = new Preguntas();
            posicionPreguntasAJugar = 0;

            // Añadimos dificultades a la lista de dificultades
            dificultades.Add("Fácil");
            dificultades.Add("Medio");
            dificultades.Add("Difícil");
            dificultades.Add("Imposible");

            // Añadimos categorias a la lista de categorias
            categorias.Add("Fútbol");
            categorias.Add("SCP");
            categorias.Add("Películas");
            categorias.Add("Marvel");
        }

        public void Examinar()
        {
            NuevaPregunta.Imagen = servicioAzure.GuardarImagenEnAzure(servicioDialogos.AbrirDialogo());
        }

        public void AñadePregunta()
        {
            Preguntas.Add(nuevaPregunta);
            NuevaPregunta = new Preguntas();
        }
        public void CargarJSON()
        {
            Preguntas = servicioJSON.CargarPreguntasDeJSON(servicioDialogos.AbrirDialogo());
        }

        public void GuardarEnJSON()
        {
            servicioJSON.GuardarPreguntasEnJSON(Preguntas, servicioDialogos.GuardarDialogo());
        }
        public void EliminarPregunta()
        {
            Preguntas.Remove(PreguntaActual);
        }

        public void NuevaPartida()
        {
            PreguntasPartida = new ObservableCollection<Preguntas>();
            posicionPreguntasAJugar = 0;
            if (Preguntas.Count > 0)
            {
                for (int i = 0; i < Categorias.Count; i++)
                {
                    for (int j = 0; j < Preguntas.Count; j++)
                    {
                        if (Preguntas[j].Dificultad == Dificultades[DificultadSeleccionada] && Preguntas[j].Categoria == Categorias[i] && !PreguntasPartida.Contains(Preguntas[j]))
                        {
                            PreguntasPartida.Add(Preguntas[j]);
                        }
                    }
                }
                PreguntaAJugar = PreguntasPartida[posicionPreguntasAJugar];
            } else
            {
                MessageBox.Show("¡No has cargado tu lista de preguntas!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Validar()
        {
            if (RespuestaIntroducida.Equals(PreguntaAJugar.Respuesta))
            {
                if (posicionPreguntasAJugar < PreguntasPartida.Count-1)
                {
                    posicionPreguntasAJugar++;
                    PreguntaAJugar = PreguntasPartida[posicionPreguntasAJugar];
                    RespuestaIntroducida = "";
                }
                else
                {
                    MessageBox.Show("¡Has completado todas las preguntas!", "¡Enhorabuena!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }
    }
}
