using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems
{
	/// <summary>
	///		Colección de <see cref="ControlItemViewModel"/>
	/// </summary>
	public class ControlGenericListViewModel<TypeData> : BaseObservableObject
	{
		// Variables privadas
		private ObservableCollection<ControlItemViewModel> _items = new ObservableCollection<ControlItemViewModel>();
		private ControlItemViewModel _selectedItem;

		/// <summary>
		///		Limpia la lista de elementos
		/// </summary>
		public void Clear()
		{
			Items.Clear();
		}

		/// <summary>
		///		Añade un elemento
		/// </summary>
		public void Add(ControlItemViewModel item, bool selected = false)
		{
			// Añade el elemento
			Items.Add(item);
			// Selecciona el elemento si es necesario
			if (selected)
				SelectedItem = item;
		}

		/// <summary>
		///		Añade un elemento al control
		/// </summary>
		public ControlItemViewModel Add(string text, TypeData tag, bool selected = false, Media.MvvmColor foreground = null)
		{
			var newItem = new ControlItemViewModel(text, tag, foreground);

				// Añade el elemento a la colección
				Add(newItem, selected);
				// Devuelve el elemento
				return newItem;
		}

		/// <summary>
		///		Inica si elemento está seleccionado
		/// </summary>
		public bool IsSelected()
		{
			return SelectedItem != null;
		}

		/// <summary>
		///		Selecciona un elemento
		/// </summary>
		public void SelectItem(TypeData item)
		{
			// Vacía el elemento seleccionado
			SelectedItem = null;
			// Selecciona el elemento
			foreach (ControlItemViewModel controlItem in Items)
				if (item.Equals(controlItem.Tag))
					SelectedItem = controlItem;
		}

		/// <summary>
		///		Obtiene el tag del elemento seleccionado
		/// </summary>
		public object GetSelectedItem(TypeData defaultValue)
		{
			if (SelectedItem == null || SelectedItem.Tag == null)
				return defaultValue;
			else
				return SelectedItem.Tag;
		}

		/// <summary>
		///		Obtiene el tag del elemento seleccionado
		/// </summary>
		public TypeItem GetSelectedItemTyped<TypeItem>(TypeItem defaultValue)
		{
			if (SelectedItem == null || SelectedItem.Tag == null)
				return defaultValue;
			else
				return (TypeItem) SelectedItem.Tag;
		}

		/// <summary>
		///		Elementos del control
		/// </summary>
		public ObservableCollection<ControlItemViewModel> Items
		{
			get { return _items; }
			set { CheckObject(ref _items, value); }
		}

		/// <summary>
		///		Elemento seleccionado
		/// </summary>
		public ControlItemViewModel SelectedItem
		{
			get { return _selectedItem; }
			set { CheckObject(ref _selectedItem, value); }
		}
	}
}
