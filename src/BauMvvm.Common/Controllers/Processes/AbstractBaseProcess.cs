using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Processes
{
	/// <summary>
	///		Clase base para los procesos
	/// </summary>
	public abstract class AbstractBaseProcess
	{ 
		// Eventos
		public event EventHandler<EventArguments.ActionEventArgs> ActionProcess;
		public event EventHandler<EventArguments.ProgressActionEventArgs> ProgressAction;
		public event EventHandler<EventArguments.EndProcessEventArgs> EndProcess;
		public event EventHandler<EventArguments.ProgressEventArgs> Progress;

		protected AbstractBaseProcess(string source, string name, string description)
		{
			Source = source;
			Name = name;
			Description = description;
		}

		/// <summary>
		///		Nombre
		/// </summary>
		public string Name { get; }

		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; }

		/// <summary>
		///		Origen del proceso
		/// </summary>
		public string Source { get; }

		/// <summary>
		///		Lanza el evento de inicio
		/// </summary>
		protected void RaiseEventStart(string message)
		{
			RaiseEvent(EventArguments.ActionEventArgs.ActionType.Start, message);
		}

		/// <summary>
		///		Lanza un evento de error
		/// </summary>
		protected void RaiseEventError(string message, Exception exception)
		{
			if (exception != null)
				message += Environment.NewLine + exception.Message;
			RaiseEvent(EventArguments.ActionEventArgs.ActionType.Error, message);
		}

		/// <summary>
		///		Lanza el evento de fin
		/// </summary>
		protected void RaiseEventEnd(string message)
		{
			RaiseEvent(EventArguments.ActionEventArgs.ActionType.Start, message);
		}

		/// <summary>
		///		Lanza un mensaje en un evento
		/// </summary>
		protected void RaiseMessageEvent(string message)
		{
			RaiseEvent(EventArguments.ActionEventArgs.ActionType.Other, message);
		}

		/// <summary>
		///		Lanza un evento
		/// </summary>
		protected void RaiseEvent(EventArguments.ActionEventArgs.ActionType action, string message)
		{
			ActionProcess?.Invoke(this, new EventArguments.ActionEventArgs(action, Source, message));
		}

		/// <summary>
		///		Lanza un evento de progreso
		/// </summary>
		protected void RaiseEventProgress(int actual, int total, string message)
		{
			Progress?.Invoke(this, new EventArguments.ProgressEventArgs(actual, total, message));
		}

		/// <summary>
		///		Lanza un evento de fin de proceso
		/// </summary>
		protected void RaiseEventEndProcess(string message, System.Collections.Generic.List<string> errors)
		{
			EndProcess?.Invoke(this, new EventArguments.EndProcessEventArgs(message, errors));
		}

		/// <summary>
		///		Lanza un evento de progreso de una acción
		/// </summary>
		protected void RaiseEventProgressAction(string id, string action, string process, long actual, long total)
		{
			ProgressAction?.Invoke(this, new EventArguments.ProgressActionEventArgs(id, Source, action, process, actual, total));
		}
	}
}