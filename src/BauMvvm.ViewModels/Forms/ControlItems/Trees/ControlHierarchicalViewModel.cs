using System;

namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.Trees
{
	/// <summary>
	///		ViewModel para un control de un elemento jerárquico
	/// </summary>
	public class ControlHierarchicalViewModel : ControlItemViewModel
	{
		// Variables privadas
		private string _type = string.Empty;
		private bool _isExpanded;
		private AsyncObservableCollection<ControlHierarchicalViewModel> _children;

		public ControlHierarchicalViewModel(ControlHierarchicalViewModel parent, string text, string type, string icon, object tag = null, 
											bool lazyLoad = true, bool isBold = false, Media.MvvmColor foreground = null) 
								: base(text, tag, isBold, foreground)
		{
			// Asigna las propiedades
			Type = type;
			Icon = icon;
			Parent = parent;
			LazyLoad = lazyLoad;
			Children = new AsyncObservableCollection<ControlHierarchicalViewModel>();
			// Si se va a tratar con una carga posterior, se añade un nodo vacío para que se muestre el signo + junto al nodo
			if (lazyLoad)
				Children.Add(new ControlHierarchicalViewModel(null, "-----", string.Empty, string.Empty, null, false));
		}

		/// <summary>
		///		Carga los elementos hijo
		/// </summary>
		public void LoadChildren()
		{
			if (IsExpanded && !IsChildrenLoaded && LazyLoad)
			{ 
				// Limpia los datos y recarga
				Children.Clear();
				LoadChildrenData();
				// Indica que se han cargado los hijos
				IsChildrenLoaded = true;
			}
		}

		/// <summary>
		///		Carga los elementos hijo
		/// </summary>
		public virtual void LoadChildrenData()
		{
		}

		/// <summary>
		///		Comprueba si dos nodos son iguales
		/// </summary>
		public virtual bool IsEquals(ControlHierarchicalViewModel node)
		{
			return Text.Equals(node.Text, StringComparison.CurrentCultureIgnoreCase) && Icon.Equals(node.Icon, StringComparison.CurrentCultureIgnoreCase);
		}

		/// <summary>
		///		Elemento padre
		/// </summary>
		public ControlHierarchicalViewModel Parent { get; }

		/// <summary>
		///		Tipo del nodo
		/// </summary>
		public string Type
		{
			get { return _type; }
			set { CheckProperty(ref _type, value); }
		}

		/// <summary>
		///		Indica si el nodo está expandido o no
		/// </summary>
		public bool IsExpanded 
		{
			get { return _isExpanded; }
			set 
			{
				if (CheckProperty(ref _isExpanded, value))
				{ 
					// Expande el elemento padre
					if (Parent != null && !Parent.IsExpanded)
						Parent.IsExpanded = true;
					// Carga los elementos hijo si no están cargados
					LoadChildren();
				}
			}
		}

		/// <summary>
		///		Indica si se han cargado los hijos
		/// </summary>
		protected bool IsChildrenLoaded { get; private set; }

		/// <summary>
		///		Indica si se debe cargar cuando se abra el nodo
		/// </summary>
		protected bool LazyLoad { get; set; }

		/// <summary>
		///		Elementos hijo
		/// </summary>
		public AsyncObservableCollection<ControlHierarchicalViewModel> Children 
		{ 
			get { return _children; }
			set { CheckObject(ref _children, value); }
		}
	}
}
