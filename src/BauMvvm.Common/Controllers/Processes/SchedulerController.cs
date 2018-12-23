using System;
using System.Collections.Generic;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Processes
{
	/// <summary>
	///		Controlador de procesos planificados
	/// </summary>
	public class SchedulerController
	{ 
		// Variables privadas
		private readonly List<AbstractPlannedProcess> _planned;
		private readonly System.Timers.Timer _scheduler;

		public SchedulerController()
		{
			_planned = new List<AbstractPlannedProcess>();
			_scheduler = new System.Timers.Timer(60000);
			_scheduler.Elapsed += (sender, evntArgs) => Process();
		}

		/// <summary>
		///		Añade un proceso a la colección
		/// </summary>
		public void AddProcess(AbstractPlannedProcess process)
		{
			_planned.Add(process);
		}

		/// <summary>
		///		Arranca el temporizador
		/// </summary>
		public void Start()
		{
			Enabled = true;
			_scheduler.Start();
		}

		/// <summary>
		///		Detiene el temporizador
		/// </summary>
		public void Stop()
		{
			Pause();
			Enabled = false;
		}

		/// <summary>
		///		Pausa el temporizador
		/// </summary>
		private void Pause()
		{
			_scheduler.Stop();
		}

		/// <summary>
		///		Arranca los procesos
		/// </summary>
		private void Process()
		{
			if (Enabled)
			{   
				// Detiene el temporizador para evitar llamada reentrantes
				Pause();
				// Realiza los procesos
				foreach (AbstractPlannedProcess process in _planned)
					if (process.MustExecute())
						process.Process();
				// Reinicia el temporizador
				_scheduler.Start();
			}
		}

		/// <summary>
		///		Indica si el planificador está activo
		/// </summary>
		public bool Enabled { get; set; }
	}
}
