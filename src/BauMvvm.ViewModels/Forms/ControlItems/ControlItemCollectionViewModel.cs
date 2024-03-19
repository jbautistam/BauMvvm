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

	/// <summary>
	///		Mueve un elemento al principio
	/// </summary>
	public void MoveToFirst(TypeData item)
	{
		if (item is not null)
		{
			// Borra el elemento de la lista
			Items.Remove(item);
			// y lo inserta al inicio
			Items.Insert(0, item);
		}
	}

	/// <summary>
	///		Mueve un elemento al final
	/// </summary>
	public void MoveToLast(TypeData item)
	{
		if (item is not null)
		{
			// Borra el elemento de la lista
			Items.Remove(item);
			// y lo añade al final
			Items.Add(item);
		}
	}

	/// <summary>
	///		Elemento seleccionado
	/// </summary>
	public TypeData? SelectedItem  { get; set; }
}
