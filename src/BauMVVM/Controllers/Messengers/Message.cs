using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Clase para envío de datos entre ViewModel
	/// </summary>
	public class Message
	{
		public Message(string source, string messageType, string action, object content)
		{
			Source = source;
			MessageType = messageType;
			Action = action;
			Content = content;
		}

		/// <summary>
		///		Fuente del mensaje
		/// </summary>
		public string Source { get; }

		/// <summary>
		///		Identificador del mensaje
		/// </summary>
		public string MessageType { get; }

		/// <summary>
		///		Acción asociada al mensaje
		/// </summary>
		public string Action { get; protected set; }

		/// <summary>
		///		Contenido del mensaje
		/// </summary>
		public object Content { get; }

		/// <summary>
		///		Fecha de alta
		/// </summary>
		public DateTime DateNew { get; } = DateTime.Now;
	}
}
