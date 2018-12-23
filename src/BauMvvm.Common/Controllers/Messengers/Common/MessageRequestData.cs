using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje de solicitud de datos
	/// </summary>
	public class MessageRequestData<TypeData> : Message
	{
		public MessageRequestData(string source, string contentRequiredType, string contentGlobalId = null)
								: base(source, "REQUESTDATA", "REQUESTDATA", null)
		{
			ContentRequiredType = contentRequiredType;
			ContentGlobalId = contentGlobalId;
		}

		/// <summary>
		///		Tipo de contenido solicitado
		/// </summary>
		public string ContentRequiredType { get; }

		/// <summary>
		///		Global ID del contenido solicitado (puede ser null si se desean todos los datos)
		/// </summary>
		public string ContentGlobalId { get; }

		/// <summary>
		///		Resultado de la solicitud
		/// </summary>
		public System.Collections.Generic.List<TypeData> Result { get; } = new System.Collections.Generic.List<TypeData>();
	}
}