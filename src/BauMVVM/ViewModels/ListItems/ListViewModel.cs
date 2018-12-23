using System;
using System.Linq;

using Bau.Libraries.BauMvvm.ViewModels;
using Bau.Libraries.LibCommonHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel para un ListView
	/// </summary>
	public class ListViewModel<TypeData> : BauMvvm.ViewModels.BaseObservableObject where TypeData : IListItemViewModel
	{ 
		// Variables privadas
		private TypeData _selectedItem = default(TypeData);

		public ListViewModel(bool changeUpdated = true) : base(changeUpdated)
		{
			RefreshCommand = new BaseCommand("Actualizar",
											 parameter => ExecuteAction(nameof(RefreshCommand), parameter),
											 parameter => CanExecuteAction(nameof(RefreshCommand), parameter));
		}

		/// <summary>
		///		Añade un elemento a la colección. Aprovecha para asignar los manejadores de eventos
		/// </summary>
		public void Add(TypeData listItem)
		{ 
			// Asigna el manejador de eventos para "SelectedItem"
			listItem.PropertyChanged += (sender, evntArgs) =>
												{
													if (evntArgs.PropertyName.EqualsIgnoreCase("IsSelected"))
														SelectedItem = listItem;
												};
			// Llama al método base
			ListItems.Add(listItem);
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected virtual void ExecuteAction(string action, object parameter)
		{
			switch (action)
			{
				case nameof(RefreshCommand):
						Refresh();
					break;
			}
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected virtual bool CanExecuteAction(string action, object parameter)
		{
			switch (action)
			{
				default:
					return true;
			}
		}

		/// <summary>
		///		Actualiza los elementos --> El raíz no hace nada
		/// </summary>
		protected virtual void Refresh()
		{
		}

		/// <summary>
		///		Obtiene el elemento seleccionado
		/// </summary>
		public TypeData SelectedItem
		{
			get
			{
				if (_selectedItem != null)
					return _selectedItem;
				else
					return ListItems.FirstOrDefault(item => item.IsSelected);
			}
			set { CheckObject(ref _selectedItem, value); }
		}

		/// <summary>
		///		Elementos de la lista
		/// </summary>
		public System.Collections.ObjectModel.ObservableCollection<TypeData> ListItems { get; } = new System.Collections.ObjectModel.ObservableCollection<TypeData>();

		/// <summary>
		///		Comando para actualización
		/// </summary>
		public BaseCommand RefreshCommand { get; }
	}
}