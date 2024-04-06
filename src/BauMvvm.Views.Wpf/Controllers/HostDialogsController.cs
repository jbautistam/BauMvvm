using System.Windows;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers;

/// <summary>
///		Controlador de cuadros de diálogo
/// </summary>
public class HostDialogsController: IHostDialogsController
{
	public HostDialogsController(string applicationName, Window mainWindow)
	{
		ApplicationName = applicationName;
		MainWindow = mainWindow;
		LastPathSelected = "C:\\";
	}

	/// <summary>
	///		Convierte el resultado de un cuadro de diálogo
	/// </summary>
	private SystemControllerEnums.ResultType ConvertDialogResult(bool? result)
	{
		if (result == null)
			return SystemControllerEnums.ResultType.Cancel;
		else if (result ?? false)
			return SystemControllerEnums.ResultType.Yes;
		else
			return SystemControllerEnums.ResultType.No;
	}

	/// <summary>
	///		Abre el cuadro de diálogo de carga de archivos
	/// </summary>
	public string? OpenDialogLoad(string? defaultPath, string filter, string? defaultFileName = null, string? defaultExtension = null)
	{
		string[]? files = OpenDialogLoadFilesMultiple(false, defaultPath, filter, defaultFileName, defaultExtension);

			// Devuelve el archivo
			if (files is not null && files.Length > 0)
				return files[0];
			else
				return null;
	}

	/// <summary>
	///		Abre el cuadro de diálogo de carga de varios archivos
	/// </summary>
	public string[]? OpenDialogLoadFiles(string defaultPath, string filter, string? defaultFileName = null, string? defaultExtension = null)
	{
		return OpenDialogLoadFilesMultiple(true, defaultPath, filter, defaultFileName, defaultExtension);
	}

	/// <summary>
	///		Abre el cuadro de diálogo de carga de varios archivos
	/// </summary>
	private string[]? OpenDialogLoadFilesMultiple(bool multiple, string? defaultPath, string filter, string? defaultFileName, string? defaultExtension)
	{
		Microsoft.Win32.OpenFileDialog file = new();

			// Asigna las propiedades
			file.Multiselect = multiple;
			file.InitialDirectory = GetDefaultPath(defaultPath);
			file.FileName = defaultFileName;
			file.DefaultExt = defaultExtension;
			file.Filter = filter;
			// Muestra el cuadro de diálogo y devuelve los archivos
			if (file.ShowDialog() ?? false)
			{
				UpdateLastPathSelected(file.FileName, false);
				return file.FileNames;
			}
			else
				return null;
	}

	/// <summary>
	///		Obtiene el directorio predeterminado (si no se le ha pasado nada, obtiene el último directorio seleccionado)
	/// </summary>
	private string? GetDefaultPath(string? defaultPath)
	{
		// Obtiene el directorio predeterminado
		if (string.IsNullOrWhiteSpace(defaultPath))
		{
			if (!string.IsNullOrWhiteSpace(LastPathSelected))
				defaultPath = LastPathSelected;
			else
				defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}
		else if (File.Exists(defaultPath))
			defaultPath = Path.GetDirectoryName(defaultPath);
		// Devuelve el directorio predeterminado
		return defaultPath;
	}

	/// <summary>
	///		Abre el cuadro de diálogo de grabación de archivos
	/// </summary>
	public string? OpenDialogSave(string? fileName, string filter, string? defaultFileName = null, string? defaultExtension = null)
	{
		Microsoft.Win32.SaveFileDialog file = new();

			// Asigna las propiedades
			file.InitialDirectory = GetDefaultPath(fileName);
			if (!string.IsNullOrWhiteSpace(defaultFileName))
				file.FileName = Path.GetFileName(defaultFileName);
			file.DefaultExt = defaultExtension;
			file.Filter = filter;
			// Muestra el cuadro de diálogo
			if (file.ShowDialog() ?? false)
			{
				// Modifica el último archivo seleccionado
				UpdateLastPathSelected(file.FileName, false);
				// Devuelve el nombre de archivo
				return file.FileName;
			}
			else
				return null;
	}

	/// <summary>
	///		Modifica el último directorio seleccionado
	/// </summary>
	private void UpdateLastPathSelected(string? fileName, bool isFolder)
	{
		if (!string.IsNullOrWhiteSpace(fileName))
		{
			if (isFolder)
				LastPathSelected = fileName;
			else
				LastPathSelected = Path.GetDirectoryName(fileName) ?? string.Empty;
		}
	}

	/// <summary>
	///		Muestra un cuadro de diálogo
	/// </summary>
	internal SystemControllerEnums.ResultType ShowDialog(Window owner, Window view)
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
	///		Abre el cuadro de diálogo de selección de un directorio
	/// </summary>
	public SystemControllerEnums.ResultType OpenDialogSelectPath(string pathSource, out string? path)
	{
		Microsoft.Win32.OpenFolderDialog dialog = new();
		SystemControllerEnums.ResultType resultType;

			// Inicializa los parámetros de salida
			path = null;
			// Inicializa las propiedads
			dialog.Multiselect = false;
			dialog.Title = "Select a folder";
			dialog.InitialDirectory = pathSource;
			// Muestra el cuadro de diálogo
			resultType = ConvertDialogResult(dialog.ShowDialog());
			// Obtiene el resultado de salida
			if (resultType == SystemControllerEnums.ResultType.Yes)
			{
				// Guarda el directorio de salida
				path = dialog.FolderName;
				// Guarda el último directorios seleccionado
				UpdateLastPathSelected(path, true);
			}
			// Devuelve el resultado
			return resultType;
	}

	/// <summary>
	///		Nombre de la aplicación
	/// </summary>
	public string ApplicationName { get; set; }

	/// <summary>
	///		Ventana principal de la aplicación
	/// </summary>
	public Window MainWindow { get; }

	/// <summary>
	///		Ultimo directorio seleccionado
	/// </summary>
	public string LastPathSelected { get; set; }
}
