using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Bau.Libraries.BauMvvm.ViewModels;

/// <summary>
///		Base para objetos observables (implementan INotifyPropertyChanged)
/// </summary>
public abstract class BaseObservableObject : INotifyPropertyChanged
{ 
	// Eventos públicos
	public event PropertyChangedEventHandler? PropertyChanged;
	// Variables privadas
	private bool _isUpdated;
	private SynchronizationContext? _contextUi = SynchronizationContext.Current;

	protected BaseObservableObject(bool changeUpdated = true)
	{
		ChangeUpdated = changeUpdated;
	}

	/// <summary>
	///		Comprueba si se debe modificar un valor de una propiedad
	/// </summary>
	protected bool CheckProperty<TypeData>(ref TypeData value, TypeData newValue, bool changeUpdated = true, [CallerMemberName] string propertyName = default!)
	{
		if (!Equals(value, newValue))
		{ 
			// Cambia el valor
			value = newValue;
			// Lanza el evento
			OnPropertyChanged(changeUpdated, propertyName);
			// Devuelve el valor que indica que se ha modificado
			return true;
		}
		else // ... los objetos son iguales y no se modifica el valor
			return false;
	}

	/// <summary>
	///		Modificar el valor de una propiedad sin marcar el objeto como modificado
	/// </summary>
	protected bool CheckPropertyNoUpdate<TypeData>(ref TypeData value, TypeData newValue, [CallerMemberName] string propertyName = default!)
	{
		return CheckProperty(ref value, newValue, false, propertyName);
	}

	/// <summary>
	///		Comprueba si se debe modificar un valor de una propiedad para un objeto
	/// </summary>
	protected bool CheckObject<TypeData>(ref TypeData? value, TypeData? newValue, bool changeUpdated = true, [CallerMemberName] string propertyName = default!)
	{
		if (!ReferenceEquals(value, newValue))
		{ 
			// Cambia el valor
			value = newValue;
			// Lanza el evento
			OnPropertyChanged(changeUpdated, propertyName);
			// Devuelve el valor que indica que se ha modificado
			return true;
		}
		else // ... los objetos son iguales y no se modifica el valor
			return false;
	}

	/// <summary>
	///		Comprueba si se debe modificar un valor de una propiedad para un objeto sin marcar el objeto como modificado
	/// </summary>
	protected bool CheckObjectNoUpdate<TypeData>(ref TypeData? value, TypeData? newValue, [CallerMemberName] string propertyName = default!)
	{
		return CheckObject(ref value, newValue, false, propertyName);
	}

	/// <summary>
	///		Ejecuta acciones dentro del contexto de sincronización
	/// </summary>
	protected void Dispatch(SendOrPostCallback action)
	{
		// Thread.CurrentThread.ExecutionContext.ExecutionContext.Send(action, new object());
		_contextUi?.Send(action, new object());
	}

	///// <summary>
	/////		Lanza el evento de cambio de propiedad
	///// </summary>
	//internal void RaiseEventPropertyChanged(string propertyName)
	//{
	//	OnPropertyChanged(propertyName);
	//}

	///// <summary>
	/////		Lanza el evento <see cref="PropertyChanged"/>
	///// </summary>
	//protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = default!)
	//{
	//	OnPropertyChanged(ChangeUpdated, propertyName);
	//}

	/// <summary>
	///		Lanza el evento <see cref="PropertyChanged"/>
	/// </summary>
	protected virtual void OnPropertyChanged(bool changeUpdated, [CallerMemberName] string propertyName = default!)
	{ 
		// Indica que se ha modificado
		//! Antes de lanzar el evento para darle la oportunidad a los objetos hijo a cambiar el valor de IsUpdated = false
		if (changeUpdated && ChangeUpdated)
			IsUpdated = true;
		// Lanza el evento
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	/// <summary>
	///		Indica si los datos del modelo se han modificado
	///		Lanza también el evento de PropertyChanged para que se pueda asociar al mismo manejador de eventos
	/// </summary>
	public virtual bool IsUpdated 
	{ 
		get { return _isUpdated; }
		set 
		{ 
			if (_isUpdated != value)
			{
				_isUpdated = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUpdated)));
			}
		}
	}

	/// <summary>
	///		Indica si se debe cambiar el valor de IsUpdated en el formulario
	/// </summary>
	public bool ChangeUpdated { get; set; }
}