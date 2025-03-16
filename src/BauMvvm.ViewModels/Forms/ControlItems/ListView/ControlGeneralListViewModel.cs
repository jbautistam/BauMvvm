using System.Collections.ObjectModel;

namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.ListView;

/// <summary>
///		Colección de <see cref="ControlItemViewModel"/>
/// </summary>
public abstract class ControlGeneralListViewModel<TypeData> : BaseObservableObject
{
	// Variables privadas
	private System.Collections.ObjectModel.ObservableCollection<TypeData> _items = [];
	private TypeData? _selectedItem;
	private bool _enabled;

	protected ControlGeneralListViewModel()
	{
		Enabled = true;
		NewItemCommand = new BaseCommand(_ => UpdateItem(default), _ => CanExecuteAction(nameof(NewItemCommand)))
										.AddListener(this, nameof(Enabled));
		OpenItemCommand = new BaseCommand(_ => UpdateItem(SelectedItem), _ => CanExecuteAction(nameof(OpenItemCommand)))
										.AddListener(this, nameof(Enabled))
										.AddListener(this, nameof(SelectedItem));
		DeleteItemCommand = new BaseCommand(_ => DeleteItem(SelectedItem), _ => CanExecuteAction(nameof(DeleteItemCommand)))
										.AddListener(this, nameof(Enabled))
										.AddListener(this, nameof(SelectedItem));
	}

	/// <summary>
	///		Modifica un elemento (virtual, lanza el evento de modificación)
	/// </summary>
	protected abstract void UpdateItem(TypeData? item);

	/// <summary>
	///		Borra un elemento (virtual, lanza el evento de borrado)
	/// </summary>
	protected abstract void DeleteItem(TypeData? item);

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
	///		Elementos del control
	/// </summary>
	public System.Collections.ObjectModel.ObservableCollection<TypeData> Items
	{
		get { return _items; }
		set { CheckObject(ref _items, value); }
	}

	/// <summary>
	///		Elemento seleccionado
	/// </summary>
	public TypeData? SelectedItem
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
}