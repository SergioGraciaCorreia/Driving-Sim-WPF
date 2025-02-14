using Driving_Sim_WPF.Logic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
		private Car _car;  // Instancia de la clase Car, que maneja el motor y la lógica de la simulación del coche
		private Image _ambientadorImage;  // Imagen que se muestra cuando el coche está parado
		private Image _ambientadorGifImage;  // Imagen GIF que se muestra cuando el coche está en movimiento

		public MainWindow()
		{
			InitializeComponent();

			// Inicialización de las imágenes que representan el ambientador
			_ambientadorImage = AmbientadorImage;
			_ambientadorGifImage = AmbientadorGifImage;

			// Crear la instancia del coche, pasándole las imágenes del fondo y los ambientadores
			_car = new Car(CarBackground, _ambientadorImage, _ambientadorGifImage);
		}

		// Evento que se dispara cuando el botón de encender el motor es presionado
		private void btnMotorOn_Click(object sender, RoutedEventArgs e)
		{
			ToggleEngineButtons(btnMotorOn);  // Cambiar el estado de los botones de encendido y apagado
			_car.StartEngine();  // Encender el motor del coche
		}

		// Evento que se dispara cuando el botón de apagar el motor es presionado
		private void btnMotorOff_Click(object sender, RoutedEventArgs e)
		{
			ToggleEngineButtons(btnMotorOff);  // Cambiar el estado de los botones de encendido y apagado
			_car.StopEngine();  // Apagar el motor del coche
		}

		// Evento que se dispara cuando el botón de acelerar es presionado
		private void btnAccelerate_Click(object sender, RoutedEventArgs e)
		{
			_car.Accelerate();  // Acelerar el coche
		}

		// Evento que se dispara cuando el botón de frenar es presionado
		private void btnBrake_Click(object sender, RoutedEventArgs e)
		{
			_car.Brake();  // Frenar el coche
		}

		// Método para alternar el estado de los botones del motor (encender / apagar)
		private void ToggleEngineButtons(ToggleButton buttonClicked)
		{
			// Si el botón presionado es el de encender, se apaga el de apagar y viceversa
			if (buttonClicked.Name == "btnMotorOn")
			{
				btnMotorOff.IsChecked = false;  // Apagar el botón de apagar
			}
			else if (buttonClicked.Name == "btnMotorOff")
			{
				btnMotorOn.IsChecked = false;  // Apagar el botón de encender
			}
		}
	}
}



