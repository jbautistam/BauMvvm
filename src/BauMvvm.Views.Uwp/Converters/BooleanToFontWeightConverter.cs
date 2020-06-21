using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace Bau.Libraries.BauMvvm.Views.Uwp.Converters
{
	/// <summary>
	///		Convierte entre valores Boolean y FontWeight
	/// </summary>
	public class BooleanToFontWeightConverter : IValueConverter
	{
		/// <summary>
		///		Convierte los valores true a FontWeightsBold y los valores false a FontWeights.Normal
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (bool) value  ? FontWeights.Bold : FontWeights.Normal;
		}

		/// <summary>
		///		Convierte el valor de vuelta
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
