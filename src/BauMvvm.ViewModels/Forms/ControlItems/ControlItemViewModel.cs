namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems;

/// <summary>
///		ViewModel para los elementos de un control (elementos de combo, listView, etc...)
/// </summary>
public class ControlItemViewModel : BaseObservableObject
{ 
	// Variables privadas
	private bool _isSelected, _isChecked, _isBold, _isItalic;
	private string _text = string.Empty, _toolTipText = string.Empty, _icon = string.Empty;
	private Media.MvvmColor _foreground = Media.MvvmColor.Black;

	public ControlItemViewModel(string text, object? tag, bool isBold = false, Media.MvvmColor? foreground = null)
	{
		Text = text;
		Tag = tag;
		IsBold = isBold;
		Foreground = foreground ?? _foreground;
	}

	/// <summary>
	///		ID del elemento
	/// </summary>
	public string Id { get; set; } = Guid.NewGuid().ToString();

	/// <summary>
	///		Texto del elemento
	/// </summary>
	public string Text 
	{
		get { return _text; }
		set { CheckProperty(ref _text, value); }
	}

	/// <summary>
	///		Texto del tooltip
	/// </summary>
	public string ToolTipText
	{
		get { return _toolTipText; }
		set { CheckProperty(ref _toolTipText, value); }
	}

	/// <summary>
	///		Indica si el elemento está en negrita
	/// </summary>
	public bool IsBold 
	{
		get { return _isBold; }
		set { CheckProperty(ref _isBold, value); }
	}

	/// <summary>
	///		Indica si el elemento está en cursiva
	/// </summary>
	public bool IsItalic
	{
		get { return _isItalic; }
		set { CheckProperty(ref _isItalic, value); }
	}

	/// <summary>
	///		Indica si el elemento está seleccionado
	/// </summary>
	public bool IsSelected 
	{
		get { return _isSelected; }
		set { CheckProperty(ref _isSelected, value); }
	}

	/// <summary>
	///		Indica si el elemento está activo
	/// </summary>
	public bool IsChecked 
	{
		get { return _isChecked; }
		set { CheckProperty(ref _isChecked, value); }
	}

	/// <summary>
	///		Icono
	/// </summary>
	public string Icon 
	{
		get { return _icon; }
		set { CheckProperty(ref _icon, value); }
	}

	/// <summary>
	///		Color del texto
	/// </summary>
	public Media.MvvmColor Foreground 
	{
		get { return _foreground; }
		set { CheckObject(ref _foreground, value); }
	}

	/// <summary>
	///		Elemento asociado
	/// </summary>
	public object? Tag { get; }
}
