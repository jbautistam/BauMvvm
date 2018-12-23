using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Processes
{
	/// <summary>
	///		Cola de tareas
	/// </summary>
	public class TasksQueue
	{   
		// Eventos
		public event EventHandler<EventArguments.ActionEventArgs> ActionProcess;
		public event EventHandler<EventArguments.ProgressActionEventArgs> ProgressAction;
		public event EventHandler<EventArguments.EndProcessEventArgs> EndProcess;
		public event EventHandler<EventArguments.ProgressEventArgs> Progress;

		/// <summary>
		///		Comprueba si existe un procesador por su tipo
		/// </summary>
		public bool ExistsByType(Type objType)
		{
			return Queue.FirstOrDefault<AbstractTask>(item => item.GetType().Equals(objType)) != null;
		}

		/// <summary>
		///		Añade una tarea a la cola y la ejecuta
		/// </summary>
		public void Process(AbstractTask processor)
		{
			Task task;

				// Añade el procesador a la cola
				Queue.Add(processor);
				// Asigna los manejador de eventos
				processor.ActionProcess += (sender, evntArgs) => ActionProcess?.Invoke(sender, evntArgs);
				processor.Progress += (sender, evntArgs) => Progress?.Invoke(sender, evntArgs);
				processor.ProgressAction += (sender, evntArgs) => ProgressAction?.Invoke(sender, evntArgs);
				processor.EndProcess += (sender, evntArgs) => TreatEndProcess(sender as AbstractTask, evntArgs);
				// Crea la tarea para la compilación en otro hilo
				task = new Task(() => processor.Process());
				// Arranca la tarea de generación
				try
				{
					task.Start();
				}
				catch (Exception exception)
				{
					TreatEndProcess(processor,
									new EventArguments.EndProcessEventArgs($"Error al lanzar el proceso{Environment.NewLine}{exception.Message}",
																		   new List<string> { exception.Message }));
				}
		}

		/// <summary>
		///		Trata el final del proceso
		/// </summary>
		private void TreatEndProcess(AbstractTask processor, EventArguments.EndProcessEventArgs evntArgs)
		{ 
			// Elimina el procesador de la cola
			Queue.Remove(processor);
			// Lanza el evento de fin de proceso
			EndProcess?.Invoke(processor, evntArgs);
		}

		/// <summary>
		///		Cola de procesos
		/// </summary>
		public List<AbstractTask> Queue { get; } = new List<AbstractTask>();
	}
}
