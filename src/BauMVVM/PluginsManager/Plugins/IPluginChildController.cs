using System;

namespace Bau.Libraries.MVVM.PluginsManager.Plugins
{
	/// <summary>
	///		Interface que deben cumplir los plugins
	/// </summary>
	public interface IPluginChildController
	{
		/// <summary>
		///		Obtiene el control de configuración
		/// </summary>
		System.Windows.Controls.UserControl GetConfigurationControl();

		/// <summary>
		///		Muestra un cuadro de diálogo
		/// </summary>
		BauMvvm.Common.Controllers.SystemControllerEnums.ResultType ShowDialog(System.Windows.Window owner, System.Windows.Window view);

		/// <summary>
		///		Inicializa las propiedades de las librerías que se asocian a otros plugin
		/// </summary>
		void InitComunicationBetweenPlugins();

		/// <summary>
		///		Inicializa las librerías del plugin
		/// </summary>
		void InitLibraries(Host.IHostPluginsController hostPluginsController);

		/// <summary>
		///		Muestra los paneles del plugin
		/// </summary>
		void ShowPanes();

		/// <summary>
		///		Nombre del plugin
		/// </summary>
		string Name { get; }

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		Host.IHostPluginsController HostPluginsController { get; }
	}
}
