using System;

namespace Bau.Libraries.MVVM.ViewModels.Forms
{
	/// <summary>
	///		Clase base para los ViewModel de un panel de herramientas
	/// </summary>
	public abstract class BasePaneViewModel : BaseFormViewModel, Interfaces.IPaneViewModel
	{ 
		// Variables privadas
		private BaseCommand _newCommand, _propertiesCommand;

		/// <summary>
		///		Comando para un nuevo elemento
		/// </summary>
		public BaseCommand NewCommand
		{
			get
			{ 
				// Asigna el comando
				if (_newCommand == null)
					_newCommand = new BaseCommand("Nuevo",
												 parameter => ExecuteAction(nameof(NewCommand), parameter),
												 parameter => CanExecuteAction(nameof(NewCommand), parameter));
				// Devuelve el comando
				return _newCommand;
			}
		}

		/// <summary>
		///		Comando para mostrar las propiedades del elemento
		/// </summary>
		public BaseCommand PropertiesCommand
		{
			get
			{ 
				// Asigna el comando
				if (_propertiesCommand == null)
					_propertiesCommand = new BaseCommand("Propiedades",
														parameter => ExecuteAction(nameof(PropertiesCommand), parameter),
														parameter => CanExecuteAction(nameof(PropertiesCommand), parameter));
				// Devuelve el comando
				return _propertiesCommand;
			}
		}
	}
}
