using System;
using System.Windows;
using System.Windows.Threading;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Tools
{
	/// <summary>
	///		Rutina de ayuda para ejecutar una acción dentro del dispatcher de la aplicación
	/// </summary>
	public static class DispatcherHelper
	{
		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		public static void Execute(Action action)
		{
			if (Application.Current is not null && Application.Current.Dispatcher is not null)
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, action);
		}
	}
}