using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de progreso común
	/// </summary>
	public class MessageProgress : Message
	{
		public MessageProgress(string id, string source, string action, string process, long actual, long total, object content)
								: base(source, "PROGRESS", action, content)
		{ 
			ID = id;
			Process = process;
			Actual = actual;
			Total = total;
		}

		/// <summary>
		///		ID del progreso
		/// </summary>
		public string ID { get; }

		/// <summary>
		///		Proceso
		/// </summary>
		public string Process { get; }

		/// <summary>
		///		Valor actual
		/// </summary>
		public long Actual { get; }

		/// <summary>
		///		Valor total
		/// </summary>
		public long Total { get; }
	}
}