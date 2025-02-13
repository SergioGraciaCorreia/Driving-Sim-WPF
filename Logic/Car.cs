using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;


namespace Driving_Sim_WPF.Logic
{
	public class Car
	{
		public bool IsEngineOn { get; private set; }
		public int CurrentSpeed { get; private set; }

		private MediaPlayer _onPlayer = new MediaPlayer();

		private MediaPlayer _idlePlayer = new MediaPlayer();

		private MediaPlayer _offPlayer = new MediaPlayer();

		public Car()
		{
			IsEngineOn = false;
			CurrentSpeed = 0;
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
				CurrentSpeed += 10;
				Console.WriteLine($"Acelerando... Velocidad actual: {CurrentSpeed} km/h");
			}
			else
			{
				Console.WriteLine("No se puede acelerar porque el motor está apagado.");
			}
		}

		// Método para frenar el coche
		public void Brake()
		{
			if (CurrentSpeed > 0)
			{
				CurrentSpeed -= 10;
				Console.WriteLine($"Frenando... Velocidad actual: {CurrentSpeed} km/h");
			}
			else
			{
				Console.WriteLine("El coche ya está detenido.");
			}
		}
	}
}

