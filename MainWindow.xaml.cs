using Driving_Sim_WPF.Logic;
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
		// Instancia de la clase Car
		private Car _car;

		public MainWindow()
		{
			InitializeComponent();
			// En MainWindow.xaml.cs
			_car = new Car(CarBackground);  // CarBackground es la referencia de la imagen en tu XAML

		}

		private void btnMotorOn_Click(object sender, RoutedEventArgs e)
		{
			// Enciende el motor
			_car.StartEngine();
		}

		private void btnMotorOff_Click(object sender, RoutedEventArgs e)
		{
			// Apaga el motor
			_car.StopEngine();
		}

		private void btnAccelerate_Click(object sender, RoutedEventArgs e)
		{
			// Acelera el coche
			_car.Accelerate();
		}

		private void btnBrake_Click(object sender, RoutedEventArgs e)
		{
			// Frena el coche
			_car.Brake();
		}
	}
}
