namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.ComboItems;

/// <summary>
///		ViewModel para un combo
/// </summary>
public class ComboViewModel : BaseObservableObject
{ 
	// Variables privadas
	private ControlItemViewModel? _selectedItem;

	public ComboViewModel(BaseObservableObject viewModelParent)
	{
		ViewModelParent = viewModelParent;
	}

	/// <summary>
	///		Añade un elemento
	/// </summary>
	public ControlItemViewModel AddItem(int? id, string text, object? tag = null)
	{
		ControlItemViewModel item = new(text, tag)
										{
											Id = id?.ToString() ?? Guid.NewGuid().ToString()
										};

			// Añade el elemento al combo
			Items.Add(item);
			// Devuelve el elemento creado
			return item;
	}

	/// <summary>
	///		Elemento seleccionado
	/// </summary>
	public ControlItemViewModel? SelectedItem
	{
		get { return _selectedItem; }
		set
		{
			if (CheckObject(ref _selectedItem, value))
				ViewModelParent.IsUpdated = true;
		}
	}

	/// <summary>
	///		Elementos
	/// </summary>
	public ControlItemCollectionViewModel<ControlItemViewModel> Items { get; } = new();

	/// <summary>
	///		Id del elemento seleccionado
	/// </summary>
	public int? SelectedId
	{
		get
		{
			if (SelectedItem is null || !int.TryParse(SelectedItem.Id, out int id))
				return null;
			else
				return id;
		}
		set
		{
			foreach (ControlItemViewModel item in Items)
				if (item.Id == value.ToString())
					SelectedItem = item;
		}
	}

	/// <summary>
	///		Objeto asociado al elemento seleccionado
	/// </summary>
	public object? SelectedTag
	{
		get
		{
			if (SelectedItem is null)
				return null;
			else
				return SelectedItem.Tag;
		}
		set
		{
			foreach (ControlItemViewModel item in Items)
				if ((value is null && item.Tag is null) ||
						(value is not null && ReferenceEquals(item.Tag, value)))
					SelectedItem = item;
		}
	}

	/// <summary>
	///		Texto seleccionado
	/// </summary>
	public string? SelectedText
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
			foreach (ControlItemViewModel item in Items)
				if (!string.IsNullOrEmpty(item.Text) && item.Text.Equals(value, StringComparison.CurrentCultureIgnoreCase))
					SelectedItem = item;
		}
	}

	/// <summary>
	///		ViewModel padre
	/// </summary>
	public BaseObservableObject ViewModelParent { get; }
}
