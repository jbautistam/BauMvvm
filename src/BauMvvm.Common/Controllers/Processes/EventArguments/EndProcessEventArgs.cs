using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Processes.EventArguments
{
	/// <summary>
	///		Argumento para los eventos de fin de proceso
	/// </summary>
	public class EndProcessEventArgs : EventArgs
	{
		public EndProcessEventArgs(string message = null, System.Collections.Generic.List<string> errors = null)
		{
			Message = message;
			Errors = errors;
		}

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; }

		/// <summary>
		///		Errores del proceso
		/// </summary>
		public System.Collections.Generic.List<string> Errors { get; }
	}
}
