using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Bau.Libraries.BauMvvm.Views.Uwp.Converters
{
	/// <summary>
	///		Convierte entre valores Boolean y Visibility
	/// </summary>
	public class BooleanToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Convierte los valores true a Visibility.Visible y los valores false a
		/// Visibility.Collapsed o a la inversa dependiendo de si el valor del parámetro es "Reverse".
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (bool) value ^ (parameter as string ?? string.Empty).Equals("Reverse") ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// Converts Visibility.Visible values to true and Visibility.Collapsed 
		/// values to false, or the reverse if the parameter is "Reverse".
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return (Visibility) value == Visibility.Visible ^ (parameter as string ?? string.Empty).Equals("Reverse");
		}
	}
}
