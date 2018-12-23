using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de log común
	/// </summary>
	public class MessageLog : Message
	{ 
		// Enumerados públicos
	  /// <summary>
	  ///		Tipo de log
	  /// </summary>
		public enum LogType
		{
			/// <summary>Mensaje informativo</summary>
			Information,
			/// <summary>Mensaje de advertencia</summary>
			Warning,
			/// <summary>Mensaje de error</summary>
			Error
		}

		public MessageLog(string source, LogType logType, string message, string description, object content)
							: base(source, "LOG", logType.ToString(), content)
		{
			Type = logType;
			Message = message;
			Description = description;
		}

		/// <summary>
		///		Tipo de log
		/// </summary>
		public LogType Type { get; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; }

		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; }
	}
}
