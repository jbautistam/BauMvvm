using System;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Interface para los controladores genéricos para las acciones sobre formularios comunes
	/// </summary>
	public interface IControllerWindow
	{
		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		ControllerWindow.ResultType ShowDialog(System.Windows.Window owner, System.Windows.Window view);

		/// <summary>
		///		Muestra una ventana con un mensaje
		/// </summary>
		void ShowMessage(string message);

		/// <summary>
		///		Muestra una ventana con una pregunta
		/// </summary>
		bool ShowQuestion(string message);

		/// <summary>
		///		Muestra una notificación
		/// </summary>
		void ShowNotification(ControllerWindow.NotificationType type, string message, string title, string urlImage = null);

		/// <summary>
		///		Muestra una pregunta con tres posibles respuestas
		/// </summary>
		ControllerWindow.ResultType ShowQuestionCancel(string message);

		/// <summary>
		///		Muestra un cuadro de diálogo para introducir un texto
		/// </summary>
		ControllerWindow.ResultType ShowInputString(string message, ref string input);

		/// <summary>
		///		Abre el cuadro de diálogo de carga
		/// </summary>
		string OpenDialogLoad(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		string[] OpenDialogLoadFilesMultiple(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo de grabación
		/// </summary>
		string OpenDialogSave(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null);

		/// <summary>
		///		Abre el cuadro de diálogo para seleccionar un directorio
		/// </summary>
		ControllerWindow.ResultType OpenDialogSelectPath(string pathource, out string path);

		/// <summary>
		///		Abre un explorador externo
		/// </summary>
		void ShowExternalBrowser(string url);
	}
}
