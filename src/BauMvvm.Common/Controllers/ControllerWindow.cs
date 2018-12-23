using System;
using System.Windows;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Controlador de ventanas comunes
	/// </summary>
	public abstract class ControllerWindow : IControllerWindow
	{
		/// <summary>
		///		Tipo de resultado del cuadro de diálogo
		/// </summary>
		public enum ResultType
		{
			Yes,
			No,
			Cancel
		}

		/// <summary>
		///		Tipo de notificación
		/// </summary>
		public enum NotificationType
		{
			/// <summary>Información</summary>
			Information,
			/// <summary>Advertencia</summary>
			Warning,
			/// <summary>Error</summary>
			Error,
			/// <summary>Otras notificaciones</summary>
			Other
		}

		public ControllerWindow(string applicationName, Window minWindow)
		{
			ApplicationName = applicationName;
			MainWindow = minWindow;
		}

		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		public ResultType ShowDialog(Window owner, Window view)
		{ 
			// Si no se le ha pasado una ventana propietario, le asigna una
			if (owner == null)
				owner = MainWindow;
			// Muestra el formulario activo
			view.Owner = owner;
			view.ShowActivated = true;
			// Muestra el formulario y devuelve el resultado
			return ConvertDialogResult(view.ShowDialog());
		}

		/// <summary>
		///		Convierte el resultado de un cuadro de diálogo
		/// </summary>
		protected ResultType ConvertDialogResult(bool? result)
		{
			if (result == null)
				return ResultType.Cancel;
			else if (result ?? false)
				return ResultType.Yes;
			else
				return ResultType.No;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de archivos
		/// </summary>
		public string OpenDialogLoad(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null)
		{
			string[] files = OpenDialogLoadFiles(false, defaultPath, filter, defaultFileName, defaultExtension);

				// Devuelve el archivo
				if (files != null && files.Length > 0)
					return files[0];
				else
					return null;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		public string[] OpenDialogLoadFilesMultiple(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null)
		{
			return OpenDialogLoadFiles(true, defaultPath, filter, defaultFileName, defaultExtension);
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		protected abstract string[] OpenDialogLoadFiles(bool multiple, string defaultPath, string filter, string defaultFileName, string defaultExtension);

		/// <summary>
		///		Abre el cuadro de diálogo de grabación de archivos
		/// </summary>
		public abstract string OpenDialogSave(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null);

		/// <summary>
		///		Muestra un cuadro de mensaje
		/// </summary>
		public void ShowMessage(string message)
		{
			MainWindow.Dispatcher.Invoke(new Action(() => MessageBox.Show(MainWindow, message, ApplicationName)),
														  System.Windows.Threading.DispatcherPriority.Normal);
		}

		/// <summary>
		///		Muestra un cuadro de mensaje
		/// </summary>
		public bool ShowQuestion(string message)
		{
			return MessageBox.Show(MainWindow, message, ApplicationName, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
		}

		/// <summary>
		///		Muestra un cuadro de mensaje para introducir un texto
		/// </summary>
		public abstract ResultType ShowInputString(string message, ref string input);

		/// <summary>
		///		Abre el cuadro de diálogo de selección de un directorio
		/// </summary>
		public abstract ResultType OpenDialogSelectPath(string pathource, out string path);

		/// <summary>
		///		Muestra una pregunta con tres posibles respuestas
		/// </summary>
		public ResultType ShowQuestionCancel(string message)
		{
			switch (MessageBox.Show(message, ApplicationName, MessageBoxButton.YesNoCancel))
			{
				case MessageBoxResult.Yes:
					return ResultType.Yes;
				case MessageBoxResult.No:
					return ResultType.No;
				default:
					return ResultType.Cancel;
			}
		}

		/// <summary>
		///		Muestra una notificación
		/// </summary>
		public virtual void ShowNotification(NotificationType type, string message, string title, string urlImage = null)
		{
			ShowMessage(message);
		}

		/// <summary>
		///		Nombre de la aplicación
		/// </summary>
		public string ApplicationName { get; set; }

		/// <summary>
		///		Ventana principal de la aplicación
		/// </summary>
		public Window MainWindow { get; }
	}
}
