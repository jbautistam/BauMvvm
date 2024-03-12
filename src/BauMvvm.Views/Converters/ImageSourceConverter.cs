using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bau.Libraries.BauMvvm.Views.Converters;

/// <summary>
///		Conversor para transformar una cadena en una imagen en memoria (evita los errores de bloqueos de imágenes
/// </summary>
public class ImageSourceConverter : IValueConverter
{
	/// <summary>
	///		Convierte un nombre de archivo en una imagen sin bloquearla
	/// </summary>
	public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{ 
		if (value is string fileName)
			return BitmapFromUri(fileName);
		else
			return null;
	}

	/// <summary>
	///		Obtiene una imagen de un archivo
	/// </summary>
	private ImageSource? BitmapFromUri(string fileName)
    {
		BitmapImage image = new();

			// Transforma el archivo en la imagen en memoria
			if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
				try
				{
					using (FileStream stream = new(fileName, FileMode.Open))
					{
						image.BeginInit();
						image.CacheOption = BitmapCacheOption.OnLoad;
						image.StreamSource = stream;
						image.EndInit();
					}
				}
				catch (Exception exception)
				{
					System.Diagnostics.Trace.TraceError($"Error when load image from file {fileName}. {exception.Message}");
				}
			// Devuelve la imagen leida
			return image;
    }

	/// <summary>
	///		Convierte un valor de imagen en nombre de archivo (no hace nada)
	/// </summary>
	public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{ 
		throw new NotImplementedException();
	}
}
