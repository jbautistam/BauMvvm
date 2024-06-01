namespace Bau.Libraries.BauMvvm.ViewModels.Forms.Interfaces;

/// <summary>
///		Interface para los ViewModel de cuadros de diálogo
/// </summary>
public interface IDialogViewModel
{
	/// <summary>
	///		Evento de cierre
	/// </summary>
	event EventHandler<EventArguments.EventCloseArgs>? Close;

	/// <summary>
	///		Comando de grabación
	/// </summary>
	public BaseCommand SaveCommand { get; }
}