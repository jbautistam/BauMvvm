using System.Collections.ObjectModel;

namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems;

/// <summary>
///		Colección de <see cref="ControlItemViewModel"/>
/// </summary>
public class ControlItemCollectionViewModel<TypeData> : ObservableCollection<TypeData>
{
	/// <summary>
	///		Sube / baja un elemento de la lista
	/// </summary>
	public void Move(TypeData item, bool moveUp)
	{
		if (item is not null)
		{
			int first = Items.IndexOf(item);
			int second = -1;

				// Obtiene los índices a intercambiar
				if (moveUp && first > 0)
					second = first - 1;
				else if (!moveUp && first < Items.Count - 1)
					second = first + 1;
				// Intercambia los índices
				if (second >= 0)
				{
					TypeData order = Items[second];

						// Cambia el orden
						Items[second] = item;
						Items[first] = order;
				}
		}
	}
}
