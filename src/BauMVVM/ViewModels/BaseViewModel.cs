using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Base para objetos observables (implementan INotifyPropertyChanged)
	/// </summary>
	public abstract class BaseViewModel : IViewModel
	{ 
		// Eventos públicos
		public event PropertyChangedEventHandler PropertyChanged;

		public BaseViewModel(Window owner = null, bool changeUpdated = true)
		{
			WindowOwner = owner;
			ChangeUpdated = changeUpdated;
		}

		/// <summary>
		///		Comprueba si se debe modificar un valor de una propiedad
		/// </summary>
		protected bool CheckProperty<TypeData>(ref TypeData value, TypeData newValue, [CallerMemberName] string property = null)
		{
			if (!object.Equals(value, newValue))
			{ 
				// Cambia el valor
				value = newValue;
				// Lanza el evento
				OnPropertyChanged(property);
				// Devuelve el valor que indica que se ha modificado
				return true;
			}
			else // ... los objetos son iguales y no se modifica el valor
				return false;
		}

		/// <summary>
		///		Comprueba si se debe modificar un valor de una propiedad para un objeto
		/// </summary>
		public bool CheckObject<TypeData>(ref TypeData value, TypeData newValue, [CallerMemberName] string property = null)
		{
			if (!ReferenceEquals(value, newValue))
			{ 
				// Cambia el valor
				value = newValue;
				// Lanza el evento
				OnPropertyChanged(property);
				// Devuelve el valor que indica que se ha modificado
				return true;
			}
			else // ... los objetos son iguales y no se modifica el valor
				return false;
		}

		/// <summary>
		///		Lanza el evento de cambio de propiedad
		/// </summary>
		internal void RaiseEventPropertyChanged(string property)
		{
			OnPropertyChanged(property);
		}

		/// <summary>
		///		Lanza el evento <see cref="PropertyChanged"/>
		/// </summary>
		protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
		{
			OnPropertyChanged(ChangeUpdated, property);
		}

		/// <summary>
		///		Lanza el evento <see cref="PropertyChanged"/>
		/// </summary>
		protected virtual void OnPropertyChanged(bool changeUpdated, [CallerMemberName] string property = null)
		{ // Indica que se ha modificado
		  //! Antes de lanzar el evento para darle la oportunidad a los objetos hijo a cambiar el valor de IsUpdated = false
			if (changeUpdated)
				IsUpdated = true;
			// Lanza el evento
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}

		/// <summary>
		///		Rutina que avisa del cierre del ViewModel por si hay que hacer alguna rutina posterior (simplemente implementa
		///	el interface y deja a los ViewModel hijo que implementen sus propias rutinas de cierre)
		/// </summary>
		public virtual void CloseViewModel()
		{
		}

		/// <summary>
		///		Indica si los datos del modelo se han modificado
		/// </summary>
		public bool IsUpdated { get; set; }

		/// <summary>
		///		Indica si se debe cambiar el valor de IsUpdated en el formulario
		/// </summary>
		public bool ChangeUpdated { get; set; }

		/// <summary>
		///		Ventana asociada al ViewModel
		/// </summary>
		public Window WindowOwner { get; set; }
	}
}