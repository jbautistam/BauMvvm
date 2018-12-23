using System;

using Bau.Libraries.LibCommonHelper.Extensors;
using Bau.Libraries.BauMvvm.ViewModels;

namespace Bau.Libraries.MVVM.ViewModels.ListItems
{
	/// <summary>
	///		Elemento base para los ViewModel de los elementos de una lista
	/// </summary>
	public class BaseListItemViewModel : BaseObservableObject, IListItemViewModel
	{   
		// Variables privadas
		private string _id, _text;
		private bool _isSelected, _isChecked;
		private object _tag;

		public BaseListItemViewModel(BaseObservableObject formParent = null)
		{
			FormParent = formParent;
			if (FormParent != null)
				PropertyChanged += (sender, evntArgs) =>
										{
											if (FormParent != null && FormParent is BaseObservableObject parent && 
														evntArgs.PropertyName.EqualsIgnoreCase(nameof(IsChecked)))
												parent.IsUpdated = true;
										};
		}

		/// <summary>
		///		Formulario padre
		/// </summary>
		public BaseObservableObject FormParent { get; } 

		/// <summary>
		///		ID del elemento del nodo
		/// </summary>
		public string ID
		{
			get { return _id; }
			set { CheckProperty(ref _id, value); }
		}

		/// <summary>
		///		Título del nodo
		/// </summary>
		public string Text
		{
			get { return _text; }
			set { CheckProperty(ref _text, value); }
		}

		/// <summary>
		///		Indica si el elemento está chequeado
		/// </summary>
		public bool IsChecked
		{
			get { return _isChecked; }
			set { CheckProperty(ref _isChecked, value); }
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
		///		Tag del elemento
		/// </summary>
		public object Tag
		{
			get { return _tag; }
			set { CheckObject(ref _tag, value); }
		}
	}
}