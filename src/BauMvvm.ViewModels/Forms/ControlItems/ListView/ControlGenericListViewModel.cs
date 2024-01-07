using System.Collections.ObjectModel;

namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.ListView;

/// <summary>
///		Colección de <see cref="ControlItemViewModel"/>
/// </summary>
public class ControlGenericListViewModel<TypeData> : BaseObservableObject
{
	// Eventos públicos
	public event EventHandler<EventArguments.CommandEventArgs<TypeData>>? Execute;
	// Variables privadas
	private ObservableCollection<ControlItemViewModel> _items = new();
	private ControlItemViewModel? _selectedItem;
	private bool _enabled;

	public ControlGenericListViewModel()
	{
		Enabled = true;
		NewItemCommand = new BaseCommand(_ => ExecuteAction(nameof(NewItemCommand)), _ => CanExecuteAction(nameof(NewItemCommand)))
										.AddListener(this, nameof(Enabled));
		OpenItemCommand = new BaseCommand(_ => ExecuteAction(nameof(OpenItemCommand)), _ => CanExecuteAction(nameof(OpenItemCommand)))
										.AddListener(this, nameof(Enabled))
										.AddListener(this, nameof(SelectedItem));
		DeleteItemCommand = new BaseCommand(_ => ExecuteAction(nameof(DeleteItemCommand)), _ => CanExecuteAction(nameof(DeleteItemCommand)))
										.AddListener(this, nameof(Enabled))
										.AddListener(this, nameof(SelectedItem));
		UpItemCommand = new BaseCommand(_ => Move(true), _ => Enabled && CanMove(true))
								.AddListener(this, nameof(Enabled))
								.AddListener(this, nameof(SelectedItem));
		DownItemCommand = new BaseCommand(_ => Move(false), _ => Enabled && CanMove(false))
								.AddListener(this, nameof(Enabled))
								.AddListener(this, nameof(SelectedItem));
	}

	/// <summary>
	///		Ejecuta una acción: lanza el evento adecuado
	/// </summary>
	private void ExecuteAction(string action)
	{
		if (action.Equals(nameof(NewItemCommand), StringComparison.CurrentCultureIgnoreCase))
			UpdateItem(default!);
		else
		{
			TypeData? item = default!;

				// Obtiene el elemento
				if (SelectedItem?.Tag is TypeData tag)
					item = tag;
				else if (SelectedItem is TypeData tagSelected)
					item = tagSelected;
				// Modifica / borra el elemento
				if (item is not null)
				{
					if (action.Equals(nameof(DeleteItemCommand), StringComparison.CurrentCultureIgnoreCase))
						DeleteItem(item);
					else
						UpdateItem(item);
				}
		}
	}

	/// <summary>
	///		Modifica un elemento (virtual, lanza el evento de modificación)
	/// </summary>
	protected virtual void UpdateItem(TypeData? item) 
	{
		if (item is null)
			Execute?.Invoke(this, new EventArguments.CommandEventArgs<TypeData>(nameof(NewItemCommand), default!));
		else
			Execute?.Invoke(this, new EventArguments.CommandEventArgs<TypeData>(nameof(OpenItemCommand), item));
	}

	/// <summary>
	///		Borra un elemento (virtual, lanza el evento de borrado)
	/// </summary>
	protected virtual void DeleteItem(TypeData item) 
	{
		Execute?.Invoke(this, new EventArguments.CommandEventArgs<TypeData>(nameof(DeleteItemCommand), item));
	}

	/// <summary>
	///		Comprueba si se puede ejecutar una acción
	/// </summary>
	private bool CanExecuteAction(string action)
	{
		return action switch
					{
						nameof(NewItemCommand) => Enabled,
						nameof(OpenItemCommand) or nameof(DeleteItemCommand) => Enabled && SelectedItem is not null,
						_ => false
					};
	}

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
	public ControlItemViewModel Add(string text, TypeData? tag, bool selected = false, bool isBold = false, Media.MvvmColor? foreground = null)
	{
		ControlItemViewModel newItem = new ControlItemViewModel(text, tag, isBold, foreground);

			// Añade el elemento a la colección
			Add(newItem, selected);
			// Devuelve el elemento
			return newItem;
	}

	/// <summary>
	///		Inica si elemento está seleccionado
	/// </summary>
	public bool IsSelected() => SelectedItem is not null;

	/// <summary>
	///		Selecciona un elemento
	/// </summary>
	public void SelectItem(TypeData item)
	{
		// Vacía el elemento seleccionado
		SelectedItem = null;
		// Selecciona el elemento
		if (item is not null)
			foreach (ControlItemViewModel controlItem in Items)
				if (item.Equals(controlItem.Tag))
					SelectedItem = controlItem;
	}

	/// <summary>
	///		Obtiene el tag del elemento seleccionado
	/// </summary>
	public object? GetSelectedItem(TypeData defaultValue)
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
	///		Elimina un elemento
	/// </summary>
	public void Remove(TypeData value)
	{
		for (int index = Items.Count - 1; index >= 0; index--)
			if (Items[index].Tag?.Equals(value) ?? false)
				Items.RemoveAt(index);
	}

	/// <summary>
	///		Comprueba si puede mover el elemento
	/// </summary>
	private bool CanMove(bool moveUp)
	{
		if (SelectedItem is null)
			return false;
		else
		{
			int index = Items.IndexOf(SelectedItem);

				return (moveUp && index > 0) || (!moveUp && index < Items.Count - 1);
		}
	}

	/// <summary>
	///		Sube / baja un elemento de la lista
	/// </summary>
	private void Move(bool moveUp)
	{
		if (SelectedItem is not null)
		{
			int first = Items.IndexOf(SelectedItem);
			int second = -1;

				// Obtiene los índices a intercambiar
				if (moveUp && first > 0)
					second = first - 1;
				else if (!moveUp && first < Items.Count - 1)
					second = first + 1;
				// Intercambia los índices
				if (second >= 0)
				{
					ControlItemViewModel order = Items[first];

						// Cambia el orden
						Items[first] = Items[second];
						Items[second] = order;
						// Selecciona el elemento
						SelectedItem = order;
						// Indica que ha habido modificaciones
						IsUpdated = true;
				}
		}
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
	public ControlItemViewModel? SelectedItem
	{
		get { return _selectedItem; }
		set { CheckObject(ref _selectedItem, value); }
	}

	/// <summary>
	///		Indica si el listview está activo para modificaciones
	/// </summary>
	public bool Enabled
	{
		get { return _enabled; }
		set { CheckProperty(ref _enabled, value); }
	}

	/// <summary>
	///		Comando para crear un nuevo elemento
	/// </summary>
	public BaseCommand NewItemCommand { get; }

	/// <summary>
	///		Comando para modificar un elemento
	/// </summary>
	public BaseCommand OpenItemCommand { get; }

	/// <summary>
	///		Comando para borrar un elemento
	/// </summary>
	public BaseCommand DeleteItemCommand { get; }

	/// <summary>
	///		Comando para subir el script
	/// </summary>
	public BaseCommand UpItemCommand { get; }

	/// <summary>
	///		Comando para bajar el script
	/// </summary>
	public BaseCommand DownItemCommand { get; }
}
