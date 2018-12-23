using System;

namespace Bau.Libraries.MVVM.Controllers.Messengers
{
	/// <summary>
	///		Controlador de mensajes
	/// </summary>
	public class MessengerController
	{   
		// Eventos públicos
		public event EventHandler<EventArgsSent> Sent;

		/// <summary>
		///		Envía un mensaje a un receptor
		/// </summary>
		public void Send(Message message)
		{
			Sent?.Invoke(this, new EventArgsSent(message));
		}

		/// <summary>
		///		Envía un mensaje a un receptor
		/// </summary>
		public void Send(string source, string action, string messageType, object content)
		{
			Send(new Message(source, messageType, action, content));
		}

		/// <summary>
		///		Lanza un mensaje de log a los receptores
		/// </summary>
		public void SendLog(string source, Common.MessageLog.LogType logType, string message, string description, object content)
		{
			Send(new Common.MessageLog(source, logType, message, description, content));
		}

		/// <summary>
		///		Lanza un mensaje para añadir archivos al MediaPlayer
		/// </summary>
		public void SendFilesToMediaPlayer(string source, string group, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> files)
		{
			Send(new Common.MessagePlayMedia(source, group, files, null));
		}

		/// <summary>
		///		Lanza un mensaje de barra de progreso a los receptores
		/// </summary>
		public void SendBarProgress(string source, string message, long actual, long total, object content)
		{
			Send(new Common.MessageBarProgress(source, message, actual, total, content));
		}

		/// <summary>
		///		Lanza un mensaje de progreso a los receptores
		/// </summary>
		public void SendProgress(string id, string source, string action, string process, long actual, long total, object content)
		{
			Send(new Common.MessageProgress(id, source, action, process, actual, total, content));
		}

		/// <summary>
		///		Lanza un mensaje con parámetros a los receptores
		/// </summary>
		public void SendParameters(string source, string messageType, string action, Settings.ParametersModelCollection parameters, object content)
		{
			Send(new Common.MessageParameters(source, messageType, action, parameters, content));
		}

		/// <summary>
		///		Envía un comando a un plugin. command es la cadena del comando del tipo: Plugin Action ParameterName=ParameterValue ParameterName=ParameterValue...
		/// </summary>
		public void SendCommand(string source, string command, object content)
		{
			Send(new Common.MessageCommand(source, command, content));
		}

		/// <summary>
		///		Envía un comando a un plugin
		/// </summary>
		public void SendCommand(string source, string targetPlugin, string action, Settings.ParametersModelCollection parameters, object content)
		{
			Send(new Common.MessageCommand(source, targetPlugin, action, parameters, content));
		}

		/// <summary>
		///		Lanza un mensaje de error a los receptores
		/// </summary>
		public void SendError(string source, string fileName, string error, int row, int column, object content)
		{
			Send(new Common.MessageError(source, fileName, error, row, column, content));
		}

		/// <summary>
		///		Envía un mensaje de último archivo abierto
		/// </summary>
		public void SendRecentFileOpened(string source, string fileName, string text)
		{
			Send(new Common.MessageRecentFileUsed(source, Common.MessageRecentFileUsed.ActionType.Open, fileName, text));
		}

		/// <summary>
		///		Envía un mensaje de Click sobre uno de los últimos archivos abiertos
		/// </summary>
		public void SendRecentFileClicked(string source, string fileName)
		{
			Send(new Common.MessageRecentFileUsed(source, Common.MessageRecentFileUsed.ActionType.Clicked, fileName, null));
		}
	}
}
