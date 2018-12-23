using System;

namespace Bau.Libraries.MVVM.Controllers.Processes.EventArguments
{
	/// <summary>
	///		Argumentos de los eventos de progreso
	/// </summary>
	public class ProgressActionEventArgs : EventArgs
	{
		public ProgressActionEventArgs(string id, string module, string action, string process, long actual, long total)
		{
			ID = id;
			Module = module;
			Action = action;
			Process = process;
			Actual = actual;
			Total = total;
		}

		/// <summary>
		///		ID del proceso
		/// </summary>
		public string ID { get; }

		/// <summary>
		///		Módulo que lanza el proceso
		/// </summary>
		public string Module { get; }

		/// <summary>
		///		Acción realizada
		/// </summary>
		public string Action { get; }

		/// <summary>
		///		Proceso
		/// </summary>
		public string Process { get; }

		/// <summary>
		///		Progreso
		/// </summary>
		public long Actual { get; }

		/// <summary>
		///		Total
		/// </summary>
		public long Total { get; }
	}
}
