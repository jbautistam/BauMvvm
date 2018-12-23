using System;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel para un ListView
	/// </summary>
	public abstract class ListControlViewModel : BauMvvm.ViewModels.Forms.BasePaneViewModel
	{ 
		// Variables privadas
		private IListItemViewModel _selectedItem;
		private ListViewItemsViewModelCollection _items;

		public ListControlViewModel(bool changeUpdated = true) : base(changeUpdated)
		{
			// Inicializa la lista de elementos
			Items = new ListViewItemsViewModelCollection();
			// Asigna la ejecución de los comandos a la modificación del elemento seleccionado
			base.PropertyChanged += (sender, args) =>
											{
												if (args.PropertyName == nameof(SelectedItem))
												{
													base.PropertiesCommand.OnCanExecuteChanged();
													base.DeleteCommand.OnCanExecuteChanged();
												}
											};
		}

		/// <summary>
		///		Carga los elementos de la lista
		/// </summary>
		protected abstract void LoadItems();

		/// <summary>
		///		Actualiza la lista
		/// </summary>
		public void Refresh()
		{
			LoadItems();
		}

		/// <summary>
		///		Elemento seleccionado en el control
		/// </summary>
		public IListItemViewModel SelectedItem
		{
			get { return _selectedItem; }
			set { CheckObject(ref _selectedItem, value); }
		}

		/// <summary>
		///		Elementos a mostrar en la lista
		/// </summary>
		public ListViewItemsViewModelCollection Items
		{
			get { return _items; }
			set { CheckObject(ref _items, value); }
		}
	}
}
