using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Tools;

/// <summary>
///		Herramientas para tratamiento de imágenes en WPF
/// </summary>
public class ImageToolsWpf
{
	/// <summary>
	///		Crea la imagen en memoria (útil para cargar una imagen desde un archivo y que no se bloquee el acceso)
	/// </summary>
	public ImageSource CreateBitmapImage(string fileName)
	{
		BitmapImage image = new BitmapImage();

			// Lee el archivo sobre la imagen
			image.BeginInit();
			image.StreamSource = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.EndInit();
			// Libera el stream para evitar excepciones de acceso al archivo cuando se intenta borrar la imagen
			image.StreamSource.Dispose();
			// Asigna la imagen
			return image;
	}
}
