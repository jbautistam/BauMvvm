using System;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Clase base para los elementos de un árbol
	/// </summary>
	public class TreeViewItemViewModel : BauMvvm.ViewModels.BaseObservableObject, ITreeViewItemViewModel
	{   
		// Variables privadas
		private string _id, _text, _image;
		private bool _isExpanded, _loadedChildren, _isSelected, _isChecked, _bold;
		private System.Windows.Media.Brush _foreground, _background;
		private System.Windows.FontWeight _fontWeight;

		public TreeViewItemViewModel(string nodeID, string text,
									 ITreeViewItemViewModel parent = null, bool lazyLoadChildren = true, object tag = null)
		{
			NodeID = nodeID;
			Text = text;
			LazyLoadChildren = lazyLoadChildren;
			Parent = parent;
			Tag = tag;
			ForegroundBrush = System.Windows.Media.Brushes.Black;
			BackgroundBrush = null;
			if (lazyLoadChildren)
				Children.Add(new TreeViewItemViewModel(null, "-----", this, false));
		}

		/// <summary>
		///		Carga los elementos hijo de este nodo
		/// </summary>
		public virtual void LoadChildren()
		{
		}

		/// <summary>
		///		Cambia las propiedades dependiendo de las propiedades del nodo
		/// </summary>
		private void SetProperties()
		{
			if (IsBold)
				FontWeightMode = System.Windows.FontWeights.Bold;
			else
				FontWeightMode = System.Windows.FontWeights.Normal;
		}

		/// <summary>
		///		Identificador del nodo
		/// </summary>
		public string NodeID
		{
			get
			{ 
				// Si no se ha definido ningún ID se crea
				if (string.IsNullOrEmpty(_id))
					_id = Guid.NewGuid().ToString();
				// Devuelve el ID
				return _id;
			}
			set { CheckProperty(ref _id, value); }
		}

		/// <summary>
		///		Texto del nodo
		/// </summary>
		public virtual string Text
		{
			get { return _text; }
			set { CheckProperty(ref _text, value); }
		}

		/// <summary>
		///		Indica si se deben cargar los hijos
		/// </summary>
		public bool LazyLoadChildren { get; protected set; }

		/// <summary>
		///		Indica si el nodo está expandido
		/// </summary>
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{   
				// Asigna el valor si se ha modificado
				if (value != _isExpanded)
				{
					_isExpanded = value;
					OnPropertyChanged();
				}
				// Si se ha expandido, expande el elemento raíz
				if (_isExpanded && Parent != null)
					Parent.IsExpanded = true;
				// Carga los elementos hijo si es necesario
				if (_isExpanded && LazyLoadChildren && !_loadedChildren)
				{ 
					// Indica que ya se han cargado los hijos
					_loadedChildren = true;
					// Carga los hijos
					Children.Clear();
					LoadChildren();
				}
			}
		}

		/// <summary>
		///		Indica si un elemento está chequeado
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
		///		Indica si el elemento se debe mostrar en negrita
		/// </summary>
		public bool IsBold
		{
			get { return _bold; }
			set
			{
				if (CheckProperty(ref _bold, value))
					SetProperties();
			}
		}

		/// <summary>
		///		Modo de visualización de un nodo (bold / normal)
		/// </summary>
		public System.Windows.FontWeight FontWeightMode
		{
			get { return _fontWeight; }
			set { CheckProperty(ref _fontWeight, value); }
		}

		/// <summary>
		///		Color del nodo
		/// </summary>
		public System.Windows.Media.Brush ForegroundBrush
		{
			get { return _foreground; }
			set { CheckObject(ref _foreground, value); }
		}

		/// <summary>
		///		Fondo del nodo
		/// </summary>
		public System.Windows.Media.Brush BackgroundBrush
		{
			get { return _background; }
			set { CheckObject(ref _background, value); }
		}

		/// <summary>
		///		Imagen
		/// </summary>
		public string Image
		{
			get { return _image; }
			set { CheckProperty(ref _image, value); }
		}

		/// <summary>
		///		Elemento padre del nodo
		/// </summary>
		public ITreeViewItemViewModel Parent { get; }

		/// <summary>
		///		Objeto adicional del nodo
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		///		Hijos del nodo
		/// </summary>
		public TreeViewItemsViewModelCollection Children { get; } = new TreeViewItemsViewModelCollection();
	}
}
