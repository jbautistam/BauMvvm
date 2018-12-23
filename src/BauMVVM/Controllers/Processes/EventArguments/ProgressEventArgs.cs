using System;

namespace Bau.Libraries.MVVM.Controllers.Processes.EventArguments
{
	/// <summary>
	///		Argumento para los eventos de progreso
	/// </summary>
	public class ProgressEventArgs : EventArgs
	{
		public ProgressEventArgs(int actual, int total, string message = null)
		{
			Actual = actual;
			Total = total;
			Message = message;
		}

		/// <summary>
		///		Actual
		/// </summary>
		public int Actual { get; private set; }

		/// <summary>
		///		Total
		/// </summary>
		public int Total { get; private set; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; private set; }
	}
}
