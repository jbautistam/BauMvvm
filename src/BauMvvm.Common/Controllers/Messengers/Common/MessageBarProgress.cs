using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de progreso común
	/// </summary>
	public class MessageBarProgress : Message
	{
		public MessageBarProgress(string source, string message, long actual, long total, object content)
								: base(source, "BARPROGRESS", "BARPROGRESS", content)
		{
			Message = message;
			Actual = actual;
			Total = total;
		}

		/// <summary>
		///		Mensaje de progreso
		/// </summary>
		public string Message { get; }

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