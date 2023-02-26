using System;
using System.Windows;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers
{
	/// <summary>
	///		Clase de ayuda para los controladores de Windows
	/// </summary>
	public class HostHelperController
	{
		public HostHelperController(Window mainWindow)
		{
			MainWindow = mainWindow;
		}

		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		public SystemControllerEnums.ResultType ShowDialog(Window owner, Window view, WindowStyle style = WindowStyle.ToolWindow)
		{ 
			// Si no se le ha pasado una ventana propietario, le asigna una
			if (owner is null)
				owner = MainWindow;
			// Muestra el formulario activo
			view.Owner = owner;
			view.ShowActivated = true;
			view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			view.WindowStyle = style;
			view.ShowInTaskbar = false;
			if (style == WindowStyle.ToolWindow)
				view.ResizeMode = ResizeMode.NoResize;
			// Muestra el formulario y devuelve el resultado
			try
			{
				return ConvertDialogResult(view.ShowDialog());
			}
			catch (Exception exception)
			{
				System.Diagnostics.Debug.WriteLine(exception);
				return SystemControllerEnums.ResultType.No;
			}
		}

		/// <summary>
		///		Muestra una ventana no modal
		/// </summary>
		public void ShowNoModal(Window owner, Window view, WindowStyle style = WindowStyle.ToolWindow)
		{
			// Si no se le ha pasado una ventana propietario, le asigna una
			if (owner is null)
				owner = MainWindow;
			// Muestra el formulario activo
			view.Owner = owner;
			view.ShowActivated = true;
			view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			view.WindowStyle = style;
			view.ShowInTaskbar = false;
			if (style == WindowStyle.ToolWindow)
				view.ResizeMode = ResizeMode.NoResize;
			// Muestra el formulario y devuelve el resultado
			try
			{
				view.Show();
			}
			catch (Exception exception)
			{
				System.Diagnostics.Debug.WriteLine(exception);
			}
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
		///		Aplicación principal
		/// </summary>
		public Application MainApp { get; }

		/// <summary>
		///		Ventana principal
		/// </summary>
		public Window MainWindow { get; }
	}
}
