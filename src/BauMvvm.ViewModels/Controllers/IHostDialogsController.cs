namespace Bau.Libraries.BauMvvm.ViewModels.Controllers;

/// <summary>
///		Controlador general para mostrar cuadros de diálogo
/// </summary>
public interface IHostDialogsController
{
	/// <summary>
	///		Abre el cuadro de diálogo de carga de archivos
	/// </summary>
	string? OpenDialogLoad(string? defaultPath, string filter, string? defaultFileName = null, string? defaultExtension = null);

	/// <summary>
	///		Abre el cuadro de diálogo de carga de varios archivos
	/// </summary>
	string[]? OpenDialogLoadFiles(string defaultPath, string filter, string? defaultFileName = null, string? defaultExtension = null);

	/// <summary>
	///		Abre el cuadro de diálogo de grabación de archivos
	/// </summary>
	string? OpenDialogSave(string? fileName, string filter, string? defaultFileName = null, string? defaultExtension = null);

	/// <summary>
	///		Abre el cuadro de diálogo de selección de un directorio
	/// </summary>
	SystemControllerEnums.ResultType OpenDialogSelectPath(string pathource, out string? path);

	/// <summary>
	///		Abre el cuadro de diálogo de selección de un directorio
	/// </summary>
	public string? OpenDialogSelectPath(string? pathSource);

	/// <summary>
	///		Ultimo directorio seleccionado
	/// </summary>
	string LastPathSelected { get; set; }
}
