using System.Windows;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers;

/// <summary>
///		Controlador de ventanas comunes
/// </summary>
public class HostSystemController: IHostSystemController
{
	public HostSystemController(string applicationName, Window mainWindow)
	{
		ApplicationName = applicationName;
		MainWindow = mainWindow;
	}

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
	public bool ShowQuestion(string message, string acceptTitle = "Aceptar", string cancelTitle = "Cancelar")
	{
		return MessageBox.Show(MainWindow, message, ApplicationName, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
	}

	/// <summary>
	///		Muestra un cuadro de mensaje para introducir un texto
	/// </summary>
	public SystemControllerEnums.ResultType ShowInputString(string message, ref string input)
	{
		SystemControllerEnums.ResultType type;
		Forms.Dialogs.InputBoxMonolineView view = new Forms.Dialogs.InputBoxMonolineView(this, message, input);

			// Muestra el cuadro de diálogo
			type = new HostDialogsController(ApplicationName, MainWindow).ShowDialog(MainWindow, view);
			// Si se ha aceptado, recoge el texto
			if (type == SystemControllerEnums.ResultType.Yes)
				input = view.InputText;
			// Devuelve el resultado
			return type;
	}

	/// <summary>
	///		Muestra un cuadro de mensaje para introducir un texto
	/// </summary>
	public SystemControllerEnums.ResultType ShowPasswordString(string message, ref string password)
	{
		SystemControllerEnums.ResultType type;
		Forms.Dialogs.InputPasswordBoxView view = new(this, message, password);

			// Muestra el cuadro de diálogo
			type = new HostDialogsController(ApplicationName, MainWindow).ShowDialog(MainWindow, view);
			// Si se ha aceptado, recoge el texto
			if (type == SystemControllerEnums.ResultType.Yes)
				password = view.Password;
			// Devuelve el resultado
			return type;
	}

	/// <summary>
	///		Muestra un cuadro de mensaje para introducir un texto multilínea
	/// </summary>
	public SystemControllerEnums.ResultType ShowInputMultilineString(string message, ref string input)
	{
		SystemControllerEnums.ResultType type;
		Forms.Dialogs.InputBoxView view = new Forms.Dialogs.InputBoxView(this, message, input);

			// Muestra el cuadro de diálogo
			type = new HostDialogsController(ApplicationName, MainWindow).ShowDialog(MainWindow, view);
			// Si se ha aceptado, recoge el texto
			if (type == SystemControllerEnums.ResultType.Yes)
				input = view.InputText;
			// Devuelve el resultado
			return type;
	}

	/// <summary>
	///		Muestra una pregunta con tres posibles respuestas
	/// </summary>
	public SystemControllerEnums.ResultType ShowQuestionCancel(string message)
	{
		return MessageBox.Show(message, ApplicationName, MessageBoxButton.YesNoCancel) switch
					{
						MessageBoxResult.Yes => SystemControllerEnums.ResultType.Yes,
						MessageBoxResult.No => SystemControllerEnums.ResultType.No,
						_ => SystemControllerEnums.ResultType.Cancel
					};
	}

	/// <summary>
	///		Muestra una notificación
	/// </summary>
	public virtual void ShowNotification(SystemControllerEnums.NotificationType type, string title, string message, TimeSpan expiration, string urlImage = null)
	{
		new Notifications.NotificationsManager().ShowNotification(type, ApplicationName, title, message, expiration, urlImage);
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
