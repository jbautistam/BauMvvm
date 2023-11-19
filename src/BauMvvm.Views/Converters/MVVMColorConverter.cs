using System.Windows.Data;
using System.Windows.Media;

using Bau.Libraries.BauMvvm.ViewModels.Media;

namespace Bau.Libraries.BauMvvm.Views.Converters;

/// <summary>
///		Conversor para <see cref="MvvmColor"/>
/// </summary>
public class MVVMColorConverter : IValueConverter
{
	/// <summary>
	///		Convierte un <see cref="MvvmColor"/> en un <see cref="Color"/>
	/// </summary>
	public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{ 
		if (value is MvvmColor source)
			return new SolidColorBrush(Color.FromArgb(source.A, source.R, source.G, source.B));
		else
			return new SolidColorBrush(Colors.Black);
	}

	/// <summary>
	///		Convierte un <see cref="Color"/> en <see cref="MvvmColor"/>
	/// </summary>
	public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{	
		throw new NotImplementedException();
	}
}
