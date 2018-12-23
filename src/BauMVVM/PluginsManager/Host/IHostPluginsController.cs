using System;

namespace Bau.Libraries.MVVM.PluginsManager.Host
{
	/// <summary>
	///		Interface para el host de plugins
	/// </summary>
	public interface IHostPluginsController : BauMvvm.Common.Interfaces.Controllers.IHostController
	{
		/// <summary>
		///		Ventana principal
		/// </summary>
		System.Windows.Window MainWindow { get; }

		/// <summary>
		///		Controlador del layout
		/// </summary>
		Views.Layout.ILayoutController LayoutController { get; }
	}
}
