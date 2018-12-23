using System;


namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje para dar comunicación de archivos
	/// </summary>
	public class MessageRecentFileUsed : Message
	{
		/// <summary>
		///		Tipo de acción
		/// </summary>
		public enum ActionType
		{
			/// <summary>Indica que se ha abierto el archivo</summary>
			Open,
			/// <summary>Indica que se ha pulsado sobre el archivo</summary>
			Clicked
		}

		public MessageRecentFileUsed(string source, ActionType action, string fileName, string text)
							: base(source, "MRU", action.ToString(), null)
		{
			Type = action;
			FileName = fileName;
			Text = text;
		}

		/// <summary>
		///		Tipo de acción
		/// </summary>
		public ActionType Type { get; }

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; }

		/// <summary>
		///		Texto a mostrar en el menú de archivos recientes
		/// </summary>
		public string Text { get; }
	}
}
