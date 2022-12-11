using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Juego_de_preguntas.Convertidores
{
    class ConvertidorRespuesta : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string respuesta = value.ToString();
                StringBuilder respuestaT = new StringBuilder(respuesta);
                Random random = new Random();
                for (int i = 0; i < respuesta.Length; i++)
                {
                    int r = random.Next(0, 3);
                    if (respuestaT[i] != ' ' && r > 0) 
                    {
                        respuestaT[i] = '_';
                    }
                }
                return respuestaT;
            }
            else
            {
                return "";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
