using System;
using Bau.Libraries.LibCommonHelper.Extensors;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mensaje con un comando
	/// </summary>
	public class MessageCommand : Message
	{
		public MessageCommand(string source, string command, object content) : base(source, "COMMAND", "EXECUTE", content)
		{
			ParseParameters(command);
		}

		public MessageCommand(string source, string targetPlugin, string action,
							  Settings.ParametersModelCollection parameters, object content) : base(source, "COMMAND", action, content)
		{
			TargetPlugin = targetPlugin;
			Parameters = parameters;
		}

		/// <summary>
		///		Interpreta los parámetros de la cadena de comando del tipo: Plugin Action ParameterName=ParameterValue ...
		/// </summary>
		/// <remarks>
		///		Por ejemplo: FtpManager Upload FtpConnection=WebsInteresantes PathLocal="C:\Users\jbautistam\Proyectos\WebSites\Generate\Ant2e6" PathRemote="/test" Mode="CheckSize"
		/// </remarks>
		private void ParseParameters(string command)
		{
			if (!command.IsEmpty())
			{ 
				// Asigna el destino
				TargetPlugin = command.Cut(" ", out command).TrimIgnoreNull();
				// Asigna la acción
				Action = command.Cut(" ", out command).TrimIgnoreNull();
				// Asigna los parámetros
				do
				{
					string name, value = "";

						// Elimina los espacios
						command = command.TrimIgnoreNull();
						// Obtiene el nombre
						name = command.Cut("=", out command);
						// Obtiene el valor
						command.TrimIgnoreNull();
						if (!command.IsEmpty())
						{
							if (!command.StartsWith("\""))
								value = command.Cut(" ", out command);
							else
							{ // Quita la comilla inicial
								command = command.Right(command.Length - 1);
								// Obtiene el valor hasta la siguiente comilla
								value = command.Cut("\"", out command);
							}
						}
						// Añade los parámetros
						if (!name.IsEmpty())
							Parameters.Add(TargetPlugin, name, value);
				}
				while (!command.IsEmpty());
			}
		}

		/// <summary>
		///		Nombre del plugin destino
		/// </summary>
		public string TargetPlugin { get; private set; }

		/// <summary>
		///		Parámetros del comando
		/// </summary>
		public Settings.ParametersModelCollection Parameters { get; } = new Settings.ParametersModelCollection();
	}
}
