using System;

using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.MVVM.Controllers.Settings
{
	/// <summary>
	///		Repository de <see cref="ParameterModel"/>
	/// </summary>
	public class ParametersRepository
	{ 
		// Constantes privadas
		private const string TagRoot = "Parameters";
		private const string TagParameter = "Parameter";
		private const string AttributeApplication = "Application";
		private const string AttributeName = "Name";

		/// <summary>
		///		Carga un archivo de parámetros
		/// </summary>
		public ParametersModelCollection Load(string fileName)
		{
			ParametersModelCollection parameters = new ParametersModelCollection();
			MLFile fileML = new XMLParser().Load(fileName);

				// Carga los datos
				if (fileML != null)
					foreach (MLNode nodeML in fileML.Nodes)
						if (nodeML.Name == TagRoot)
							foreach (MLNode childML in nodeML.Nodes)
								if (childML.Name == TagParameter)
									parameters.Add(new ParameterModel(childML.Attributes[AttributeApplication].Value,
																	  childML.Attributes[AttributeName].Value,
																	  childML.Value));
				// Devuelve los parámetros
				return parameters;
		}

		/// <summary>
		///		Graba un archivo de parámetros
		/// </summary>
		public void Save(string fileName, ParametersModelCollection parameters)
		{
			MLFile fileML = new MLFile();
			MLNode nodeML = fileML.Nodes.Add(TagRoot);

				// Añade los nodos
				foreach (ParameterModel parameter in parameters)
				{
					MLNode childML = nodeML.Nodes.Add(TagParameter, parameter.Value);

						// Añade los atributos del nodo
						childML.Attributes.Add(AttributeApplication, parameter.Application);
						childML.Attributes.Add(AttributeName, parameter.Name);
				}
				// Graba el archivo
				new XMLWriter().Save(fileName, fileML);
		}
	}
}