using System;

using Bau.Libraries.BauMvvm.ViewModels;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		ViewModel de un ListView modificable
	/// </summary>
	public abstract class ListViewUpdatableViewModel<TypeData> : ListViewModel<TypeData> where TypeData : IListItemViewModel
	{ 
		// Variables privadas
		private bool _itemsUpdated = false;

		public ListViewUpdatableViewModel(bool changeUpdated = true) : base(changeUpdated)
		{
			NewItemCommand = new BaseCommand("Insertar",
											 parameter => ExecuteAction(nameof(NewItemCommand), parameter),
											 parameter => CanExecuteAction(nameof(NewItemCommand), parameter));
			UpdateItemCommand = new BaseCommand("Modificar",
												parameter => ExecuteAction(nameof(UpdateItemCommand), parameter),
												parameter => CanExecuteAction(nameof(UpdateItemCommand), parameter))
									.AddListener(this, "SelectedItem");
			DeleteItemCommand = new BaseCommand("Borrar",
												parameter => ExecuteAction(nameof(DeleteItemCommand), parameter),
												parameter => CanExecuteAction(nameof(DeleteItemCommand), parameter))
									.AddListener(this, "SelectedItem");
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected override void ExecuteAction(string action, object parameter)
		{
			switch (action)
			{
				case nameof(NewItemCommand):
						if (NewItem())
							ItemsUpdated = true;
					break;
				case nameof(UpdateItemCommand):
						if (SelectedItem != null)
						{
							if (UpdateItem(SelectedItem))
								ItemsUpdated = true;
						}
					break;
				case nameof(DeleteItemCommand):
						if (SelectedItem != null)
						{
							if (DeleteItem(SelectedItem))
								ItemsUpdated = true;
						}
					break;
				default:
						base.ExecuteAction(action, parameter);
					break;
			}
		}

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected override bool CanExecuteAction(string action, object parameter)
		{
			switch (action)
			{
				case nameof(NewItemCommand):
					return true;
				case nameof(UpdateItemCommand):
				case nameof(DeleteItemCommand):
					return SelectedItem != null;
				default:
					return base.CanExecuteAction(action, parameter);
			}
		}

		/// <summary>
		///		Crea un nuevo elemento
		/// </summary>
		protected abstract bool NewItem();

		/// <summary>
		///		Modifica el elemento seleccionado. Se le pasa null si es un elemento nuevo
		/// </summary>
		protected abstract bool UpdateItem(TypeData selectedItem);

		/// <summary>
		///		Borra un elemento
		/// </summary>
		protected abstract bool DeleteItem(TypeData selectedItem);

		/// <summary>
		///		Comando de nuevo elemento
		/// </summary>
		public BaseCommand NewItemCommand { get; }

		/// <summary>
		///		Comando de nuevo elemento
		/// </summary>
		public BaseCommand UpdateItemCommand { get; }

		/// <summary>
		///		Comando para borrado de un elemento
		/// </summary>
		public BaseCommand DeleteItemCommand { get; }

		/// <summary>
		///		Indica si los elementos se han modificado (añadido, modificado o eliminado)
		/// </summary>
		public bool ItemsUpdated
		{
			get { return _itemsUpdated; }
			set { CheckProperty(ref _itemsUpdated, value); }
		}
	}
}

