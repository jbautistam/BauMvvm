using System;

using Bau.Libraries.LibCommonHelper.Extensors;
using Bau.Libraries.BauMvvm.ViewModels;

namespace Bau.Libraries.MVVM.ViewModels.ComboItems
{
	/// <summary>
	///		ViewModel para un combo
	/// </summary>
	public class ComboViewModel : BaseObservableObject
	{ 
		// Variables privadas
		private readonly string _propertyView;
		private ComboItem _selectedItem;

		public ComboViewModel(BaseObservableObject viewModelParent, string propertyView)
		{
			ViewModelParent = viewModelParent;
			_propertyView = propertyView;
		}

		/// <summary>
		///		Añade un elemento
		/// </summary>
		public void AddItem(int? id, string text, object tag = null)
		{
			Items.Add(id, text, tag);
		}

		/// <summary>
		///		Elemento seleccionado
		/// </summary>
		public ComboItem SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				if (CheckObject(ref _selectedItem, value))
				{
					//ViewModelParent.RaiseEventPropertyChanged(_propertyView);
					ViewModelParent.IsUpdated = true;
				}
			}
		}

		/// <summary>
		///		Elementos
		/// </summary>
		public ComboItemsCollection Items { get; } = new ComboItemsCollection();

		/// <summary>
		///		ID del elemento seleccionado
		/// </summary>
		public int? SelectedID
		{
			get
			{
				if (SelectedItem == null)
					return null;
				else
					return SelectedItem.ID;
			}
			set
			{
				foreach (ComboItem item in Items)
					if (item.ID == value)
						SelectedItem = item;
			}
		}

		/// <summary>
		///		Objeto asociado al elemento seleccionado
		/// </summary>
		public object SelectedTag
		{
			get
			{
				if (SelectedItem == null)
					return null;
				else
					return SelectedItem.Tag;
			}
			set
			{
				foreach (ComboItem item in Items)
					if ((value == null && item.Tag == null) ||
							(value != null && ReferenceEquals(item.Tag, value)))
						SelectedItem = item;
			}
		}

		/// <summary>
		///		Texto seleccionado
		/// </summary>
		public string SelectedText
		{
			get
			{
				if (SelectedItem == null)
					return null;
				else
					return SelectedItem.Text;
			}
			set
			{
				foreach (ComboItem item in Items)
					if (item.Text.EqualsIgnoreCase(value))
						SelectedItem = item;
			}
		}

		/// <summary>
		///		ViewModel padre
		/// </summary>
		public BaseObservableObject ViewModelParent { get; }
	}
}
