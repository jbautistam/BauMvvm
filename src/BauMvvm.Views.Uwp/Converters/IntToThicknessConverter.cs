using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Bau.Libraries.BauMvvm.Views.Uwp.Converters
{
	/// <summary>
	///		Convierte entre valores int y Thickness (por ejemplo para el margen)
	/// </summary>
	public class IntToThicknessConverter : IValueConverter
	{
		/// <summary>
		/// Convierte los valores true a Visibility.Visible y los valores false a
		/// Visibility.Collapsed o a la inversa dependiendo de si el valor del parámetro es "Reverse".
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			try
			{
				return new Thickness((int) value, 0, 0, 0);
			}
			catch 
			{
				return null;
			}
		}

		/// <summary>
		///		Convierte el valor de vuelta
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return null;
		}
	}
}
