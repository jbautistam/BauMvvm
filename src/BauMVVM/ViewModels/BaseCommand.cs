using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Bau.Libraries.MVVM.ViewModels
{
	/// <summary>
	///		Clase base para los comandos
	/// </summary>
	public class BaseCommand : ICommand
	{ 
		// Eventos públicos
		public event EventHandler CanExecuteChanged;
		// Variables privadas
		private readonly Action<object> _actionExecute = null;
		private readonly Predicate<object> _predicateCanExecute = null;
		private GenericWeakEventManager<PropertyChangedEventArgs> _weakEventManager = null;

		public BaseCommand(Action<object> execute, Predicate<object> canExecute = null,
						   INotifyPropertyChanged source = null, string propertyName = null) 
					: this(null, execute, canExecute, source, propertyName)
		{
		}

		public BaseCommand(string caption, Action<object> execute, Predicate<object> canExecute = null,
						   INotifyPropertyChanged source = null, string propertyName = null)
		{
			Caption = caption;
			_actionExecute = execute;
			_predicateCanExecute = canExecute;
			if (source != null)
				AddListener(source, propertyName);
		}

		/// <summary>
		///		Ejecuta un comando
		/// </summary>
		public void Execute(object parameter)
		{
			_actionExecute?.Invoke(parameter);
		}

		/// <summary>
		///		Comprueba si se puede ejecutar un comando
		/// </summary>
		public bool CanExecute(object parameter)
		{
			if (_predicateCanExecute != null)
				return _predicateCanExecute(parameter);
			else
				return true;
		}

		/// <summary>
		///		Añade un listener de eventos al comando para una propiedad
		/// </summary>
		public BaseCommand AddListener<TEntity>(INotifyPropertyChanged source, Expression<Func<TEntity, object>> property)
		{
			return AddListener(source, GetPropertyName(property));
		}

		/// <summary>
		///		Añade un listener de eventos al comando para un nombre de propiedad
		/// </summary>
		public BaseCommand AddListener(INotifyPropertyChanged source, string propertyName)
		{ 
			// Añade el listener
			PropertyChangedEventManager.AddListener(source, WeakEventManager, propertyName);
			// Devuelve este objeto (permite un interface fluent)
			return this;
		}

		/// <summary>
		///		Traduce una expresión lambda en un nombre de propiedad / método
		/// </summary>
		private string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> expression)
		{
			LambdaExpression lambda = expression as LambdaExpression;
			MemberExpression memberExpression;
			ConstantExpression constantExpression;
			PropertyInfo propertyInfo;

				// Obtiene la expresión
				if (lambda.Body is UnaryExpression)
					memberExpression = (lambda.Body as UnaryExpression).Operand as MemberExpression;
				else
					memberExpression = lambda.Body as MemberExpression;
				// Obtiene una expresión constante
				constantExpression = memberExpression.Expression as ConstantExpression;
				// Obtiene la información de la propiedad
				propertyInfo = memberExpression.Member as PropertyInfo;
				// Obtiene el nombre de la propiedad
				return propertyInfo.Name;
		}

		/// <summary>
		///		Rutina a la que se llama para lanzar el evento de modificación de CanExecute
		/// </summary>
		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///		Crea un manejador para INotifyPropertyChanged 
		/// </summary>
		private void RequeryCanExecute(object sender, PropertyChangedEventArgs evntArgsPropertyChanged)
		{
			OnCanExecuteChanged();
		}

		/// <summary>
		///		Título del comando
		/// </summary>
		public string Caption { get; }

		/// <summary>
		///		Manager de WeakEvents
		/// </summary>
		private GenericWeakEventManager<PropertyChangedEventArgs> WeakEventManager
		{
			get
			{ 
				// Crea el manager de eventos
				if (_weakEventManager == null)
					_weakEventManager = new GenericWeakEventManager<PropertyChangedEventArgs>(RequeryCanExecute);
				// Devuelve el manager de eventos
				return _weakEventManager;
			}
		}
	}
}
