using Juego_de_preguntas.VistasModelo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Juego_de_preguntas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowVM();
            this.DataContext = vm;
        }

        private void Examinar_Button(object sender, RoutedEventArgs e)
        {
            vm.Examinar(imagenUrl_TextBox);
        }

        private void AñadePregunta_Button(object sender, RoutedEventArgs e)
        {
            vm.AñadePregunta(imagenUrl_TextBox.Text);
        }

        private void LimpiarFormulario_Button(object sender, RoutedEventArgs e)
        {

        }
    }
}
