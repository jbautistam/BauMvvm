using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de error común
	/// </summary>
	public class MessageError : Message
	{
		public MessageError(string source, string fileName, string error, int row, int column, object content)
							: base(source, "ERROR", "ERROR", content)
		{
			FileName = fileName;
			Error = error;
			Row = row;
			Column = column;
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; }

		/// <summary>
		///		Error
		/// </summary>
		public string Error { get; }

		/// <summary>
		///		Fila
		/// </summary>
		public int Row { get; }

		/// <summary>
		///		Columna
		/// </summary>
		public int Column { get; }
	}
}
