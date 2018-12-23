using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers
{
	/// <summary>
	///		Argumentos del evento de envío de mensajes
	/// </summary>
	public class EventArgsSent : EventArgs
	{
		public EventArgsSent(Message message)
		{
			MessageSent = message;
		}

		/// <summary>
		///		Mensaje enviado
		/// </summary>
		public Message MessageSent { get; }
	}
}
