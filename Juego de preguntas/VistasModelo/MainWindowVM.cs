using CommunityToolkit.Mvvm.ComponentModel;
using Juego_de_preguntas.Modelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Juego_de_preguntas.VistasModelo
{
    class MainWindowVM : ObservableObject, INotifyCollectionChanged
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

        private Partida partida;

        public Partida Partida
        {
            get { return partida; }
            set { partida = value; }
        }


        private Preguntas preguntaAJugar;

        public Preguntas PreguntaAJugar
        {
            get { return preguntaAJugar; }
            set { SetProperty(ref preguntaAJugar, value); }
        }
        private int posicionPreguntasAJugar;
        private string respuestaIntroducida;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public string RespuestaIntroducida
        {
            get { return respuestaIntroducida; }
            set { SetProperty(ref respuestaIntroducida, value); }
        }

        private double categoriaFutbol;

        public double CategoriaFutbol
        {
            get { return categoriaFutbol; }
            set { SetProperty(ref categoriaFutbol, value); }
        }

        private double categoriaSCP;

        public double CategoriaSCP
        {
            get { return categoriaSCP; }
            set { SetProperty(ref categoriaSCP, value); }
        }

        private double categoriaCine;

        public double CategoriaCine
        {
            get { return categoriaCine; }
            set { SetProperty(ref categoriaCine, value); }
        }

        private double categoriaMarvel;

        public double CategoriaMarvel
        {
            get { return categoriaMarvel; }
            set { SetProperty(ref categoriaMarvel, value); }
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
            Partida = new Partida();

            CategoriaCine = 0.5;
            CategoriaFutbol = 0.5;
            CategoriaMarvel = 0.5;
            CategoriaSCP = 0.5;

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
            if (NuevaPregunta.Pregunta != null && NuevaPregunta.Respuesta != null && 
                NuevaPregunta.Dificultad != null && NuevaPregunta.Categoria != null && 
                NuevaPregunta.Imagen != null)
            {
                Preguntas.Add(NuevaPregunta);
                NuevaPregunta = new Preguntas();
                MessageBox.Show("Pregunta añadida correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            MessageBox.Show("Pregunta eliminada correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void NuevaPartida()
        {
            ResetearValores();
            if (Preguntas.Count > 0)
            {
                for (int i = 0; i < Categorias.Count; i++)
                {
                    for (int j = 0; j < Preguntas.Count; j++)
                    {
                        if (Preguntas[j].Dificultad == Partida.Dificultad && Preguntas[j].Categoria == Categorias[i] && !PreguntasPartida.Contains(Preguntas[j]))
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
                ValidaCategoria();
                if (posicionPreguntasAJugar < PreguntasPartida.Count-1)
                {
                    posicionPreguntasAJugar++;
                    PreguntaAJugar = PreguntasPartida[posicionPreguntasAJugar];
                    RespuestaIntroducida = "";
                }
                else
                {
                    MessageBox.Show("¡Has completado todas las preguntas!", "¡Enhorabuena!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Partida.Empezada = true;
                }
            }
        }

        public void ValidaCategoria()
        {
            if (PreguntasPartida[posicionPreguntasAJugar].Categoria == "Fútbol")
            {
                CategoriaFutbol = 1;
            }
            else if (PreguntasPartida[posicionPreguntasAJugar].Categoria == "SCP")
            {
                CategoriaSCP = 1;
            }
            else if (PreguntasPartida[posicionPreguntasAJugar].Categoria == "Películas")
            {
                CategoriaCine = 1;
            }
            else
            {
                CategoriaMarvel = 1;
            }
        }

        public void ResetearValores()
        {
            RespuestaIntroducida = "";
            PreguntasPartida = new ObservableCollection<Preguntas>();
            posicionPreguntasAJugar = 0;
            Partida.Dificultad = Dificultades[DificultadSeleccionada];
            Partida.Empezada = false;

            CategoriaCine = 0.5;
            CategoriaFutbol = 0.5;
            CategoriaSCP = 0.5;
            CategoriaMarvel = 0.5;
        }

        public void LimpiarFormulario()
        {
            NuevaPregunta = null;
        }
    }
}
