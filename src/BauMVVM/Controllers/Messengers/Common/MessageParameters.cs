using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje con un diccionario de parámetros
	/// </summary>
	public class MessageParameters : Message
	{
		public MessageParameters(string source, string messageType, string action, Settings.ParametersModelCollection parameters, object content)
							: base(source, messageType, action, content)
		{ 
			Parameters = parameters;
		}

		/// <summary>
		///		Parámetros del mensaje
		/// </summary>
		public Settings.ParametersModelCollection Parameters { get; }
	}
}
