using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Processes.EventArguments
{
	/// <summary>
	///		Eventos de sincronización
	/// </summary>
	public class ActionEventArgs : EventArgs
	{
		/// <summary>
		///		Tipo de acción
		/// </summary>
		public enum ActionType
		{
			/// <summary>Arranca el proceso</summary>
			Start,
			/// <summary>Otros procesos</summary>
			Other,
			/// <summary>Error</summary>
			Error,
			/// <summary>Finaliza el proceso</summary>
			End
		}

		public ActionEventArgs(ActionType type, string module, string message)
		{
			Action = type;
			Module = module;
			Message = message;
		}

		/// <summary>
		///		Módulo que lanza el proceso
		/// </summary>
		public string Module { get; }

		/// <summary>
		///		Tipo de acción
		/// </summary>
		public ActionType Action { get; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; }
	}
}
