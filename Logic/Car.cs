using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows; // Para usar el control Image

using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Driving_Sim_WPF.Logic
{
	public class Car
	{
		// Propiedades del coche: si el motor está encendido y la velocidad actual
		public bool IsEngineOn { get; private set; }
		public int CurrentSpeed { get; private set; }

		// Reproductores de medios para los sonidos (motor encendido, inactivo y apagado)
		private MediaPlayer _onPlayer = new MediaPlayer();
		private MediaPlayer _idlePlayer = new MediaPlayer();
		private MediaPlayer _offPlayer = new MediaPlayer();

		// Temporizador para el cambio de imágenes del fondo
		private DispatcherTimer _imageChangeTimer;
		private int _minInterval = 32;
		private int _interval = 1000;

		// Imágenes relacionadas con el coche y el ambientador
		private Image CarBackground;
		private Image AmbientadorImage;
		private Image AmbientadorGifImage;

		// Decodificador y frames del GIF del ambientador
		private GifBitmapDecoder _ambientadorGifDecoder;
		private ImageSource _ambientadorGifFrames;
		private Storyboard _ambientadorStoryboard;

		// Rutas a los archivos de imagen y audio
		private readonly string ambientadorPng = "pack://application:,,,/assets/ambientador.png";
		private readonly string ambientadorGif = "pack://application:,,,/assets/ambientador.gif";

		// Constructor: Inicializa el coche con las imágenes proporcionadas
		public Car(Image carBackground, Image ambientadorImage, Image ambientadorGifImage)
		{
			IsEngineOn = false;
			CurrentSpeed = 0;
			CarBackground = carBackground;
			AmbientadorImage = ambientadorImage;
			AmbientadorGifImage = ambientadorGifImage;

			// Inicializamos el temporizador para cambiar las imágenes de fondo del coche
			_imageChangeTimer = new DispatcherTimer();
			_imageChangeTimer.Tick += ImageChangeTimer_Tick;
			_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);

			// Cargar las imágenes del ambientador
			AmbientadorImage.Source = new BitmapImage(new Uri(ambientadorPng));
			_ambientadorGifDecoder = new GifBitmapDecoder(
				new Uri(ambientadorGif, UriKind.Absolute),
				BitmapCreateOptions.PreservePixelFormat,
				BitmapCacheOption.Default);

			// Mostrar el primer frame del GIF
			_ambientadorGifFrames = _ambientadorGifDecoder.Frames[0];
			AmbientadorGifImage.Source = _ambientadorGifFrames;
		}

		// Método para encender el motor del coche
		public void StartEngine()
		{
			if (!IsEngineOn)
			{
				IsEngineOn = true;

				// Reproducir sonido de encendido del motor
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "encendido.mp3");
				if (File.Exists(path))
				{
					_onPlayer.Open(new Uri(path, UriKind.Absolute));
					_onPlayer.Volume = 1.0;
					_onPlayer.Play();
				}

				// Reproducir sonido de motor inactivo
				string idlePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "motoridle.mp3");
				if (File.Exists(idlePath))
				{
					_idlePlayer.Open(new Uri(idlePath));
					_idlePlayer.MediaEnded += (s, e) => _idlePlayer.Position = TimeSpan.Zero;
					_idlePlayer.Play();
				}
			}
		}

		// Método para apagar el motor del coche
		public void StopEngine()
		{
			if (IsEngineOn)
			{
				IsEngineOn = false;
				CurrentSpeed = 0;

				// Detener el temporizador y los sonidos
				_imageChangeTimer.Stop();
				_onPlayer.Stop();
				_idlePlayer.Stop();

				// Reproducir sonido de apagado del motor
				string offPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "apagado.mp3");
				if (File.Exists(offPath))
				{
					_offPlayer.Open(new Uri(offPath));
					_offPlayer.Play();
				}

				UpdateAmbientador();
			}
		}

		// Método para acelerar el coche
		public void Accelerate()
		{
			if (IsEngineOn)
			{
				// Aumentar la velocidad y ajustar la frecuencia de cambio de fondo
				CurrentSpeed += 30;
				_interval = Math.Max(_minInterval, 1000 - CurrentSpeed * 10);
				_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);

				if (!_imageChangeTimer.IsEnabled)
				{
					_imageChangeTimer.Start();
				}

				UpdateAmbientador();
			}
		}

		// Método para frenar el coche
		public void Brake()
		{
			if (CurrentSpeed > 0)
			{
				// Reducir la velocidad y ajustar la frecuencia de cambio de fondo
				CurrentSpeed -= 30;

				if (CurrentSpeed < 0)
				{
					CurrentSpeed = 0;
				}

				_interval = Math.Max(_minInterval, 1000 - CurrentSpeed * 10);
				_imageChangeTimer.Interval = TimeSpan.FromMilliseconds(_interval);

				if (!_imageChangeTimer.IsEnabled)
				{
					_imageChangeTimer.Start();
				}

				UpdateAmbientador();
			}
			else
			{
				// Detener el temporizador si no hay velocidad
				_imageChangeTimer.Stop();
			}
		}

		// Método para cambiar el fondo del coche (ciclo de imágenes)
		private void ImageChangeTimer_Tick(object sender, EventArgs e)
		{
			if (CarBackground.Source.ToString().Contains("Coche-Fondo1.png"))
			{
				CarBackground.Source = new BitmapImage(new Uri("pack://application:,,,/assets/Coche-Fondo2.png"));
			}
			else
			{
				CarBackground.Source = new BitmapImage(new Uri("pack://application:,,,/assets/Coche-Fondo1.png"));
			}
		}

		// Actualiza la visibilidad del ambientador dependiendo de la velocidad del coche
		public void UpdateAmbientador()
		{
			if (CurrentSpeed >= 10)
			{
				AmbientadorImage.Visibility = System.Windows.Visibility.Hidden;
				AmbientadorGifImage.Visibility = System.Windows.Visibility.Visible;
				StartGifAnimation();
			}
			else
			{
				AmbientadorGifImage.Visibility = System.Windows.Visibility.Hidden;
				AmbientadorImage.Visibility = System.Windows.Visibility.Visible;
			}
		}

		// Inicia la animación del GIF del ambientador
		private void StartGifAnimation()
		{
			_ambientadorStoryboard = new Storyboard
			{
				RepeatBehavior = RepeatBehavior.Forever // Hacer la animación continua
			};

			// Creamos la animación de los fotogramas
			for (int i = 0; i < _ambientadorGifDecoder.Frames.Count; i++)
			{
				// Por un error en la creación del GIF, el fotograma 16 está vacío
				// El fotograma 16 está vacío, así que lo omitimos
				if (i == 15)  // Consideramos que el frame 16 es el índice 15 (base cero)
				{
					continue; // Saltamos el fotograma vacío
				}

				var frame = _ambientadorGifDecoder.Frames[i];
				var keyFrame = new DiscreteObjectKeyFrame(
					frame,
					KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100 * i))
				);

				var animation = new ObjectAnimationUsingKeyFrames
				{
					Duration = new Duration(TimeSpan.FromSeconds(_ambientadorGifDecoder.Frames.Count / 10.0)),
					RepeatBehavior = RepeatBehavior.Forever
				};
				animation.KeyFrames.Add(keyFrame);

				// Añadimos la animación al Storyboard
				_ambientadorStoryboard.Children.Add(animation);

				// Establecemos el objetivo de la animación (la imagen)
				Storyboard.SetTarget(animation, AmbientadorGifImage);
				Storyboard.SetTargetProperty(animation, new PropertyPath(Image.SourceProperty));
			}

			// Iniciar la animación del storyboard
			_ambientadorStoryboard.Begin();
		}
	}
}




