using System;

using Bau.Libraries.BauMvvm.Common.Controllers;

namespace Bau.Libraries.MVVM.PluginsManager.Plugins
{
	/// <summary>
	///		Base para los controladores de plugins
	/// </summary>
	public abstract class BasePluginController : IPluginChildController
	{
		protected BasePluginController() : this(null, null) { }

		protected BasePluginController(Host.IHostPluginsController hostPluginsController, string name)
		{
			HostPluginsController = hostPluginsController;
			Name = name;
		}

		/// <summary>
		///		Muestra un cuadro de diálogo sobre la ventana principal
		/// </summary>
		public SystemControllerEnums.ResultType ShowDialog(System.Windows.Window view)
		{
			return ShowDialog(HostPluginsController.MainWindow, view);
		}

		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		public SystemControllerEnums.ResultType ShowDialog(System.Windows.Window owner, System.Windows.Window view)
		{ 
			// Muestra el formulario activo
			view.Owner = owner;
			view.ShowActivated = true;
			// Muestra el formulario y devuelve el resultado
			return ConvertDialogResult(view.ShowDialog());
		}

		/// <summary>
		///		Convierte el resultado de un cuadro de diálogo
		/// </summary>
		//TODO --> Esto también está definido en el HostSystemController. Habría que marcarlo en el interface pero ShowDialog depende de System.Windows
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
		///		Inicializa las librerías del plugin
		/// </summary>
		public abstract void InitLibraries(Host.IHostPluginsController hostPluginsController);

		/// <summary>
		///		Inicializa las propiedades de las librerías que se asocian a otros plugin
		/// </summary>
		public virtual void InitComunicationBetweenPlugins()
		{ 
			// No hace nada, crea una interface
		}

		/// <summary>
		///		Muestra los paneles del plugin
		/// </summary>
		public abstract void ShowPanes();

		/// <summary>
		///		Obtiene el control de configuración
		/// </summary>
		public abstract System.Windows.Controls.UserControl GetConfigurationControl();

		/// <summary>
		///		Nombre del plugin
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		public Host.IHostPluginsController HostPluginsController { get; set; }
	}
}
