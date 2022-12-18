using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_de_preguntas.Modelo
{
    class Partida : ObservableObject
    {

		private bool empezada;

		public bool Empezada
		{
			get { return empezada; }
			set { SetProperty(ref empezada, value); }
		}


		private string dificultadPartida;
		public string Dificultad
		{
			get { return dificultadPartida; }
			set { SetProperty(ref dificultadPartida, value); }
		}

		public Partida()
		{
			this.Empezada = true;
		}

		public Partida(string dificultad)
		{
            this.Empezada = true;
            this.dificultadPartida = dificultad;
		}
	}
}
