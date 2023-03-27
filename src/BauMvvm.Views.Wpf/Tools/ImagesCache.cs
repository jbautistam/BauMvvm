using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Tools
{
	/// <summary>
	///		Caché de imágenes
	/// </summary>
	public class ImagesCache
	{
		/// <summary>
		///		Carga la imagen o la obtiene desde la caché
		/// </summary>
		public ImageSource GetImage(string key, bool loadFromResource)
		{
			// Carga la imagen de la caché, si no la encuentra, la carga del recurso
			if (!Images.TryGetValue(key, out ImageSource image))
				try
				{
					// Carga la imagen
					if (loadFromResource)
						image = new ImageToolsWpf().GetFromResource(key);
					else
						image = new ImageToolsWpf().GetFromFileName(key);
					// La añade al diccionario
					Images.Add(key, image);
				}
				catch (Exception exception)
				{
					System.Diagnostics.Debug.WriteLine($"Error when load image: {key}", exception.Message);
				}
			// Devuelve la imagen
			return image;
		}

		/// <summary>
		///		Imágenes en la caché
		/// </summary>
		private Dictionary<string, ImageSource> Images { get; } = new(StringComparer.InvariantCultureIgnoreCase);
	}
}
