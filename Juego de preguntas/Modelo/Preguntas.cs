using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_de_preguntas.Modelo
{
    class Preguntas : ObservableObject
    {
        private string pregunta;

        public string Pregunta
        {
            get { return pregunta; }
            set { SetProperty(ref pregunta, value); }
        }

        private string respuesta;

        public string Respuesta
        {
            get { return respuesta; }
            set { SetProperty(ref respuesta, value); }
        }
        
        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        private string dificultad;

        public string Dificultad
        {
            get { return dificultad; }
            set { SetProperty(ref dificultad, value); }
        }

        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { SetProperty(ref categoria, value); }
        }

        public Preguntas(){ }

        public Preguntas(string pregunta, string respuesta, string imagen, string dificultad, string categoria)
        {
            this.Pregunta = pregunta;
            this.Respuesta = respuesta;
            this.Imagen = imagen;
            this.Dificultad = dificultad;
            this.Categoria = categoria;
        }
    }
}
