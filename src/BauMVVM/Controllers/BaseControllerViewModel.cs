using System;

namespace Bau.Libraries.MVVM.Controllers
{
	/// <summary>
	///		Clase base para los controladores de ViewModel
	/// </summary>
	public abstract class BaseControllerViewModel
	{
		public BaseControllerViewModel(string moduleName, PluginsManager.Host.IHostController hostController, IControllerViews viewController)
		{
			ModuleName = moduleName;
			HostController = hostController;
			ViewsController = viewController;
		}

		/// <summary>
		///		Inicializa la aplicación
		/// </summary>
		public abstract void InitModule();

		/// <summary>
		///		Inicializa la solicitud de información a otros plugins
		/// </summary>
		public virtual void InitComunicationBetweenPlugins()
		{ 
			// ... no hace nada, simplemente crea el esqueleto de la función
		}

		/// <summary>
		///		Obtiene un parámetro
		/// </summary>
		protected string GetParameter(string name)
		{
			if (HostController != null)
				return HostController.Configuration.Parameters.GetValue(ModuleName, name);
			else
				return "";
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		/// <remarks>
		///		No tiene un if MainController == null porque me interesa que lance una excepción en ejecución si no se ha definido el controlador
		///	de la aplicación principal
		/// </remarks>
		protected void SetParameter(string name, string value)
		{
			HostController.Configuration.Parameters.SetValue(ModuleName, name, value);
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		protected void SetParameter(string name, int value)
		{
			SetParameter(name, value.ToString());
		}

		/// <summary>
		///		Asigna un parámetro
		/// </summary>
		protected void SetParameter(string name, bool value)
		{
			SetParameter(name, value.ToString());
		}

		/// <summary>
		///		Nombre del módulo
		/// </summary>
		public string ModuleName { get; }

		/// <summary>
		///		Controlador de la aplicación principal
		/// </summary>
		public PluginsManager.Host.IHostController HostController { get; }

		/// <summary>
		///		Controlador de vistas
		/// </summary>
		public virtual IControllerViews ViewsController { get; protected set; }
	}
}
