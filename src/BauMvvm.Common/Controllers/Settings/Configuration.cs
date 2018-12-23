using System;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Settings
{
	/// <summary>
	///		Clase de configuración de las aplicaciones
	/// </summary>
	public class Configuration
	{
		// Variables privadas
		private ParametersModelCollection _parameters;
		private string _pathBaseConfiguration;

		public Configuration(string shortApplicationName, string pathBaseData)
		{
			ShortApplicationName = shortApplicationName;
			PathBaseData = pathBaseData;
		}

		/// <summary>
		///		Carga la configuración
		/// </summary>
		public void Load()
		{
			Parameters = new ParametersRepository().Load(FileName);
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		public void Save()
		{
			new ParametersRepository().Save(FileName, Parameters);
		}

		/// <summary>
		///		Nombre corto de la aplicación
		/// </summary>
		public string ShortApplicationName { get; }

		/// <summary>
		///		Directorio de datos de la aplicación
		/// </summary>
		public string PathBaseData
		{
			get { return _pathBaseConfiguration; }
			set
			{
				if (string.IsNullOrEmpty(value))
					_pathBaseConfiguration = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ShortApplicationName);
				else
					_pathBaseConfiguration = value;
			}
		}

		/// <summary>
		///		Directorio de la aplicación
		/// </summary>
		public string PathBaseApplication
		{
			get { return AppDomain.CurrentDomain.BaseDirectory; }
		}

		/// <summary>
		///		Nombre de archivo de configuración
		/// </summary>
		public string FileName
		{
			get { return System.IO.Path.Combine(PathBaseData, "Configuration.xml"); }
		}

		/// <summary>
		///		Paramétros de configuración
		/// </summary>
		public ParametersModelCollection Parameters
		{
			get
			{
				// Carga los parámetros
				if (_parameters == null)
					Load();
				// Devuelve los parámetros
				return _parameters;
			}
			private set { _parameters = value; }
		}
	}
}
