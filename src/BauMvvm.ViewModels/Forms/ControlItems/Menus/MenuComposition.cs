namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.Menus;

/// <summary>
///		Clase con los datos de los menús y barras de herramientas
/// </summary>
public class MenuComposition
{
	/// <summary>
	///		Menús
	/// </summary>
	public MenuGroupViewModelCollection Menus { get; } = new();

	/// <summary>
	///		Barras de herramientas
	/// </summary>
	public MenuGroupViewModelCollection ToolBars { get; } = new();
}
