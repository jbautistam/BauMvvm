using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Bau.Libraries.LibCommonHelper.Extensors;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Model para un treeView
	/// </summary>
	public abstract class TreeViewModel : BauMvvm.ViewModels.Forms.BasePaneViewModel
	{ 
		// Variables privadas
		private ITreeViewItemViewModel _selectedItem;

		public TreeViewModel(bool changeUpdated = true) : base(changeUpdated)
		{
			PropertyChanged += (sender, args) =>
											{
												if (args.PropertyName == nameof(SelectedItem))
												{
													PropertiesCommand.OnCanExecuteChanged();
													DeleteCommand.OnCanExecuteChanged();
												}
											};
		}

		/// <summary>
		///		Obtiene los elementos seleccionados
		/// </summary>
		public List<ITreeViewItemViewModel> GetSelectedItems()
		{
			return GetSelectedItems(Nodes);
		}

		/// <summary>
		///		Obtiene recursivamente los elementos seleccionados
		/// </summary>
		private List<ITreeViewItemViewModel> GetSelectedItems(ObservableCollection<ITreeViewItemViewModel> nodes)
		{
			List<ITreeViewItemViewModel> selectedItems = new List<ITreeViewItemViewModel>();

				// Recorre los nodos obteniendo los seleccionados
				foreach (TreeViewItemViewModel node in nodes)
				{ 
					// Añade el nodo si está seleccionado
					if (node.IsSelected)
						selectedItems.Add(node);
					// Añade los nodos hijo
					if (node.Children != null && node.Children.Count > 0)
						selectedItems.AddRange(GetSelectedItems(node.Children));
				}
				// Devuelve la colección de nodos
				return selectedItems;
		}

		/// <summary>
		///		Carga los nodos hijo
		/// </summary>
		protected abstract TreeViewItemsViewModelCollection LoadNodes();

		/// <summary>
		///		Actualiza el árbol
		/// </summary>
		public virtual void Refresh()
		{
			List<Tuple<string, string>> nodesExpanded = GetNodesExpanded(Nodes);

				// Limpia los nodos
				Nodes.Clear();
				// Carga los nodos
				Nodes.AddRange(LoadNodes());
				// Vuelve a expandir los elementos
				ExpandNodes(Nodes, nodesExpanded);
		}

		/// <summary>
		///		Obtiene recursivamente los elementos seleccionados
		/// </summary>
		private List<Tuple<string, string>> GetNodesExpanded(TreeViewItemsViewModelCollection nodes)
		{
			List<Tuple<string, string>> expanded = new List<Tuple<string, string>>();

				// Recorre los nodos obteniendo los seleccionados
				foreach (TreeViewItemViewModel node in nodes)
				{ 
					// Añade el nodo si se ha expandido
					if (node.IsExpanded)
						expanded.Add(new Tuple<string, string>(node.GetType().ToString(), node.NodeID));
					// Añade los nodos hijo
					if (node.Children != null && node.Children.Count > 0)
						expanded.AddRange(GetNodesExpanded(node.Children));
				}
				// Devuelve la colección de nodos
				return expanded;
		}

		/// <summary>
		///		Expande un nodo
		/// </summary>
		private void ExpandNodes(TreeViewItemsViewModelCollection nodes, List<Tuple<string, string>> nodesExpanded)
		{
			if (nodes != null)
				foreach (ITreeViewItemViewModel node in nodes)
					if (Checkexpanded(node, nodesExpanded))
					{ 
						// Expande el nodo
						node.IsExpanded = true;
						// Expande los hijos
						ExpandNodes(node.Children, nodesExpanded);
					}
		}

		/// <summary>
		///		Comprueba si un nodo estaba en la lista de nodos abiertos
		/// </summary>
		private bool Checkexpanded(ITreeViewItemViewModel node, List<Tuple<string, string>> nodesExpanded)
		{ 
			// Recorre la colección
			foreach (Tuple<string, string> nodeExpanded in nodesExpanded)
				if (nodeExpanded.Item1.EqualsIgnoreCase(node.GetType().ToString()) &&
						nodeExpanded.Item2.EqualsIgnoreCase(node.NodeID))
					return true;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return false;
		}

		/// <summary>
		///		Debug de los nodos
		/// </summary>
		protected void Debug()
		{
			System.Diagnostics.Debug.WriteLine("Debug de TreeViewModel");
			Nodes.Debug();
		}

		/// <summary>
		///		Elemento seleccionado en el árbol
		/// </summary>
		public ITreeViewItemViewModel SelectedItem
		{
			get { return _selectedItem; }
			set { CheckObject(ref _selectedItem, value); }
		}

		/// <summary>
		///		Nodos a mostrar en el árbol
		/// </summary>
		public abstract TreeViewItemsViewModelCollection Nodes { get; set; }
	}
}
