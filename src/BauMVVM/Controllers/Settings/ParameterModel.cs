using System;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Clase con los datos de un parámetro
	/// </summary>
	public class ParameterModel
	{
		public ParameterModel(string application, string name, string value)
		{ 
			Application = application;
			Name = name;
			Value = value;
		}

		/// <summary>
		///		Nombre de aplicación
		/// </summary>
		public string Application { get; }

		/// <summary>
		///		Nombre del parámetro
		/// </summary>
		public string Name { get; }

		/// <summary>
		///		Valor del parámetro
		/// </summary>
		public string Value { get; set; }
	}
}
