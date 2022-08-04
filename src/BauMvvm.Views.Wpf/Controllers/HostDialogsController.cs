using System;
using System.Windows;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers
{
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
		public string OpenDialogLoad(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null)
		{
			string[] files = OpenDialogLoadFilesMultiple(false, defaultPath, filter, defaultFileName, defaultExtension);

				// Devuelve el archivo
				if (files != null && files.Length > 0)
					return files[0];
				else
					return null;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		public string[] OpenDialogLoadFiles(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null)
		{
			return OpenDialogLoadFilesMultiple(true, defaultPath, filter, defaultFileName, defaultExtension);
		}

		/// <summary>
		///		Abre el cuadro de diálogo de carga de varios archivos
		/// </summary>
		private string[] OpenDialogLoadFilesMultiple(bool multiple, string defaultPath, string filter, string defaultFileName, string defaultExtension)
		{
			Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();

				// Asigna las propiedades
				file.Multiselect = multiple;
				file.InitialDirectory = GetDefaultPath(defaultPath);
				file.FileName = defaultFileName;
				file.DefaultExt = defaultExtension;
				file.Filter = filter;
				// Muestra el cuadro de diálogo y devuelve los archivos
				if (file.ShowDialog() ?? false)
				{
					LastPathSelected = System.IO.Path.GetDirectoryName(file.FileName);
					return file.FileNames;
				}
				else
					return null;
		}

		/// <summary>
		///		Obtiene el directorio predeterminado (si no se le ha pasado nada, obtiene el último directorio seleccionado)
		/// </summary>
		private string GetDefaultPath(string defaultPath)
		{
			// Obtiene el directorio predeterminado
			if (string.IsNullOrWhiteSpace(defaultPath))
			{
				if (!string.IsNullOrWhiteSpace(LastPathSelected))
					defaultPath = LastPathSelected;
				else
					defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			// Devuelve el directorio predeterminado
			return defaultPath;
		}

		/// <summary>
		///		Abre el cuadro de diálogo de grabación de archivos
		/// </summary>
		public string OpenDialogSave(string defaultPath, string filter, string defaultFileName = null, string defaultExtension = null)
		{
			Microsoft.Win32.SaveFileDialog file = new Microsoft.Win32.SaveFileDialog();

				// Asigna las propiedades
				file.InitialDirectory = GetDefaultPath(defaultPath);
				file.FileName = defaultFileName;
				file.DefaultExt = defaultExtension;
				file.Filter = filter;
				// Muestra el cuadro de diálogo
				if (file.ShowDialog() ?? false)
				{
					LastPathSelected = System.IO.Path.GetDirectoryName(file.FileName);
					return file.FileName;
				}
				else
					return null;
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
		public SystemControllerEnums.ResultType OpenDialogSelectPath(string pathSource, out string path)
		{
			Ookii.Dialogs.Wpf.VistaFolderBrowserDialog folder = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			SystemControllerEnums.ResultType type;

				// Inicializa los valores de salida
				path = null;
				// Asigna la carpeta inicial
				folder.SelectedPath = GetDefaultPath(pathSource);
				folder.ShowNewFolderButton = true;
				// Muestra el diálogo
				type = ConvertDialogResult(folder.ShowDialog());
				// Obtiene el directorio
				if (type == SystemControllerEnums.ResultType.Yes)
				{
					path = folder.SelectedPath;
					LastPathSelected = path;
				}
				// Devuelve el resultado
				return type;
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
		public string LastPathSelected { get; private set; }
	}
}
