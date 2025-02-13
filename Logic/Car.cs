using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging; // Para usar el control Image

namespace Driving_Sim_WPF.Logic
{
	public class Car
	{
		public bool IsEngineOn { get; private set; }
		public int CurrentSpeed { get; private set; }

		private MediaPlayer _onPlayer = new MediaPlayer();
		private MediaPlayer _idlePlayer = new MediaPlayer();
		private MediaPlayer _offPlayer = new MediaPlayer();

		private DispatcherTimer _imageChangeTimer;
		private int _minInterval = 200; // Intervalo mínimo entre cambios de imagen (en milisegundos)
		private int _interval = 1000; // Intervalo inicial (1 segundo)

		private Image CarBackground; // Referencia a la imagen

		public Car(Image carBackground)
		{
			IsEngineOn = false;
			CurrentSpeed = 0;
			CarBackground = carBackground; // Asignamos la imagen pasada

			// Inicializamos el temporizador para cambiar las imágenes
			_imageChangeTimer = new DispatcherTimer();
			_imageChangeTimer.Tick += ImageChangeTimer_Tick;
			_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);
		}

		// Método para encender el motor
		public void StartEngine()
		{
			if (!IsEngineOn) // Solo si está apagado
			{
				IsEngineOn = true;

				// Ruta absoluta desde la carpeta bin
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "encendido.mp3");

				if (File.Exists(path))
				{
					_onPlayer.Open(new Uri(path, UriKind.Absolute));
					_onPlayer.Volume = 1.0;
					_onPlayer.Play();
				}

				// Reproducir motor en bucle tras el encendido
				string idlePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "motoridle.mp3");
				if (File.Exists(idlePath))
				{
					_idlePlayer.Open(new Uri(idlePath));
					_idlePlayer.MediaEnded += (s, e) => _idlePlayer.Position = TimeSpan.Zero; // Repetir en bucle
					_idlePlayer.Play();
				}
			}
		}

		// Método para apagar el motor
		public void StopEngine()
		{
			if (IsEngineOn)
			{
				IsEngineOn = false;
				CurrentSpeed = 0;  // Al apagar el motor, la velocidad se resetea

				// Detenemos el temporizador
				_imageChangeTimer.Stop();

				_onPlayer.Stop();
				_idlePlayer.Stop();
				_offPlayer.Open(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "apagado.mp3")));
				_offPlayer.Play();
			}
		}

		// Método para acelerar el coche
		public void Accelerate()
		{
			if (IsEngineOn)
			{
				// Aumentamos la velocidad
				CurrentSpeed += 10;

				// Reducimos el intervalo entre cambios de imagen a medida que aumenta la velocidad
				_interval = Math.Max(_minInterval, 1000 - CurrentSpeed * 10); // Por ejemplo, 1000ms - velocidad*10

				// Actualizamos el intervalo del temporizador
				_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);

				// Iniciar el temporizador si no está en marcha
				if (!_imageChangeTimer.IsEnabled)
				{
					_imageChangeTimer.Start();
				}
			}
		}

		// Método para frenar el coche
		public void Brake()
		{
			if (CurrentSpeed > 0)
			{
				// Reducimos la velocidad
				CurrentSpeed -= 10;

				// Aseguramos que la velocidad no sea negativa
				if (CurrentSpeed < 0)
				{
					CurrentSpeed = 0;
				}

				// Aumentamos el intervalo entre cambios de imagen a medida que disminuye la velocidad
				_interval = Math.Max(_minInterval, 1000 - CurrentSpeed * 10); // Por ejemplo, 1000ms - velocidad*10

				// Actualizamos el intervalo del temporizador
				_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);

				// Iniciar el temporizador si no está en marcha
				if (!_imageChangeTimer.IsEnabled)
				{
					_imageChangeTimer.Start();
				}
			}
			else
			{
				// Detenemos el temporizador si la velocidad es 0
				_imageChangeTimer.Stop();
			}
		}


		private void ImageChangeTimer_Tick(object sender, EventArgs e)
		{
			// Alternamos entre las imágenes
			if (CarBackground.Source.ToString().Contains("Coche-Fondo1.png"))
			{
				CarBackground.Source = new BitmapImage(new Uri("pack://application:,,,/assets/Coche-Fondo2.png"));
			}
			else
			{
				CarBackground.Source = new BitmapImage(new Uri("pack://application:,,,/assets/Coche-Fondo1.png"));
			}
		}
	}
}


