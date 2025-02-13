using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Driving_Sim_WPF
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnMotorOn_Click(object sender, RoutedEventArgs e)
		{
			// Solo se imprime un mensaje para previsualizar el evento
			Console.WriteLine("Botón Motor ON presionado");
		}

		private void btnMotorOff_Click(object sender, RoutedEventArgs e)
		{
			// Solo se imprime un mensaje para previsualizar el evento
			Console.WriteLine("Botón Motor OFF presionado.");
		}

		private void btnAccelerate_Click(object sender, RoutedEventArgs e)
		{
			// Solo se imprime un mensaje para previsualizar el evento
			Console.WriteLine("Botón Acelerar presionado.");
		}

		private void btnBrake_Click(object sender, RoutedEventArgs e)
		{
			// Solo se imprime un mensaje para previsualizar el evento
			Console.WriteLine("Botón Frenar presionado.");
		}
	}
}
