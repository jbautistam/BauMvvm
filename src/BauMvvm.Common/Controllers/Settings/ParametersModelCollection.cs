using System;
using System.Collections.Generic;
using System.Linq;

using Bau.Libraries.LibCommonHelper.Extensors;

namespace Bau.Libraries.BauMvvm.Common.Controllers.Settings
{
	/// <summary>
	///		Colección de <see cref="ParameterModel"/>
	/// </summary>
	public class ParametersModelCollection : List<ParameterModel>
	{
		/// <summary>
		///		Añade / modifica el valor de un parámetro
		/// </summary>
		public void Add(string application, string name, string value)
		{
			ParameterModel parameter = Search(application, name);

				// Si no existe el parámetro, lo añade, si existe cambia su valor
				if (parameter == null)
				{ 
					// Crea el parámetro
					parameter = new ParameterModel(application, name, value);
					// Lo añade a la colección
					Add(parameter);
				}
				else
					parameter.Value = value;
		}

		/// <summary>
		///		Busca un parámetro en la colección
		/// </summary>
		public ParameterModel Search(string application, string name)
		{
			return this.FirstOrDefault(parameter => parameter.Application.EqualsIgnoreCase(application) &&
													parameter.Name.EqualsIgnoreCase(name));
		}

		/// <summary>
		///		Obtiene el valor de un parámetro
		/// </summary>
		public string GetValue(string application, string name)
		{
			ParameterModel parameter = Search(application, name);

				if (parameter == null)
					return "";
				else
					return parameter.Value;
		}

		/// <summary>
		///		Asigna un valor a un parámetro
		/// </summary>
		public void SetValue(string application, string name, string value)
		{
			Add(application, name, value);
		}

		/// <summary>
		///		Asigna un valor a un parámetro
		/// </summary>
		public void SetValue(string application, string name, double dblValue)
		{
			SetValue(application, name, dblValue.ToString().Replace(',', '.'));
		}
	}
}