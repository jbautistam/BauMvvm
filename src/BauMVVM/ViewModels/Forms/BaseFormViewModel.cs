using System;

namespace Bau.Libraries.MVVM.ViewModels.Forms
{
	/// <summary>
	///		ViewModel para las clases de ventana
	/// </summary>
	public abstract class BaseFormViewModel : BaseViewModel, Interfaces.IFormViewModel
	{
		// Eventos públicos
		public event EventHandler<EventArguments.CloseEventArgs> RequestClose;

		public BaseFormViewModel(System.Windows.Window owner = null, bool changeUpdated = true) : base(owner, changeUpdated)
		{
			CloseCommand = new BaseCommand("Cerrar", parameter => Close(Controllers.ControllerWindow.ResultType.No));
			RefreshCommand = new BaseCommand("Actualizar",
											 parameter => ExecuteAction(nameof(RefreshCommand), parameter),
											 parameter => CanExecuteAction(nameof(RefreshCommand), parameter));
			SaveCommand = new BaseCommand("Guardar",
										  parameter => ExecuteAction(nameof(SaveCommand), parameter),
										  parameter => CanExecuteAction(nameof(SaveCommand), parameter));
			DeleteCommand = new BaseCommand("Borrar",
											parameter => ExecuteAction(nameof(DeleteCommand), parameter),
											parameter => CanExecuteAction(nameof(DeleteCommand), parameter));
		}

		/// <summary>
		///		Ejecuta una acción
		/// </summary>
		protected abstract void ExecuteAction(string action, object parameter);

		/// <summary>
		///		Comprueba si se puede ejecutar una acción
		/// </summary>
		protected abstract bool CanExecuteAction(string action, object parameter);

		/// <summary>
		///		Llama al evento de cierre de ventana
		/// </summary>
		public virtual void Close(Controllers.ControllerWindow.ResultType result)
		{
			RequestClose?.Invoke(this, new EventArguments.CloseEventArgs(result));
		}

		/// <summary>
		///		Menús
		/// </summary>
		public Menus.MenuComposition MenuCompositionData { get; } = new Menus.MenuComposition();

		/// <summary>
		///		Comando de cierre de la ventana
		/// </summary>
		public BaseCommand CloseCommand { get; }

		/// <summary>
		///		Comando para actualización
		/// </summary>
		public BaseCommand RefreshCommand { get; }

		/// <summary>
		///		Comando para grabación
		/// </summary>
		public BaseCommand SaveCommand { get; }

		/// <summary>
		///		Comando para borrado
		/// </summary>
		public BaseCommand DeleteCommand { get; }
	}
}
