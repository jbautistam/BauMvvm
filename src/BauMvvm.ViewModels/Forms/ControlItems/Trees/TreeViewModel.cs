namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.Trees;

/// <summary>
///		ViewModel para la presentación de un árbol
/// </summary>
public abstract class TreeViewModel : BaseObservableObject
{
	// Variables privadas
	private AsyncObservableCollection<ControlHierarchicalViewModel> _children = new();
	private ControlHierarchicalViewModel? _selectedNode;

	protected TreeViewModel()
	{
		ContextUI = SynchronizationContext.Current ?? new SynchronizationContext();
	}

	/// <summary>
	///		Carga los nodos
	/// </summary>
	public virtual void Load()
	{
		object state = new object();

			// Carga los nodos en el árbol
			//? _contexUi mantiene el contexto de sincronización que creó el ViewModel (que debería ser la interface de usuario)
			//? Al generarse las tablas en otro hilo o desde un evento, no se puede borrar ObservableCollection sin una
			//? excepción del tipo "Este tipo de CollectionView no admite cambios en su SourceCollection desde un hilo diferente del hilo Dispatcher"
			//? Por eso se tiene que añadir el mensaje de log desde el contexto de sincronización de la UI
			ContextUI.Send(_ => {
									List<ControlHierarchicalViewModel> nodesExpanded = GetNodesExpanded(Children);

										// Limpia la colección de hijos
										Children.Clear();
										// Añade los nodos raíz
										AddRootNodes();
										// Expande los nodos previamente abiertos
										ExpandNodes(Children, nodesExpanded);
								},
								state
						  );
	}

	/// <summary>
	///		Añade los nodos raíz
	/// </summary>
	protected abstract void AddRootNodes();

	/// <summary>
	///		Obtiene recursivamente los elementos expandidos del árbol (para poder recuperarlos posteriormente y dejarlos abiertos de nuevo)
	/// </summary>
	protected List<ControlHierarchicalViewModel> GetNodesExpanded(AsyncObservableCollection<ControlHierarchicalViewModel> nodes)
	{
		List<ControlHierarchicalViewModel> expanded = new List<ControlHierarchicalViewModel>();

			// Recorre los nodos obteniendo los seleccionados
			foreach (ControlHierarchicalViewModel node in nodes)
			{ 
				// Añade el nodo si se ha expandido
				if (node.IsExpanded)
					expanded.Add(node);
				// Añade los nodos hijo
				if (node.Children != null && node.Children.Count > 0)
					expanded.AddRange(GetNodesExpanded(node.Children));
			}
			// Devuelve la colección de nodos
			return expanded;
	}

	/// <summary>
	///		Expande todos los nodos
	/// </summary>
	protected void ExpandAll()
	{
		ExpandAll(Children);
	}

	/// <summary>
	///		Expande todos los nodos
	/// </summary>
	private void ExpandAll(AsyncObservableCollection<ControlHierarchicalViewModel> nodes)
	{
		foreach (ControlHierarchicalViewModel node in nodes)
		{
			// Expande el nodo
			node.IsExpanded = true;
			// Expande los hijos
			ExpandAll(node.Children);
		}
	}

	/// <summary>
	///		Expande los nodos que se le pasan en la colección <param name="nodesExpanded" />
	/// </summary>
	protected void ExpandNodes(AsyncObservableCollection<ControlHierarchicalViewModel> nodes, List<ControlHierarchicalViewModel> nodesExpanded)
	{ 
		if (nodes is not null)
			foreach (ControlHierarchicalViewModel node in nodes)
				if (CheckIsExpanded(node, nodesExpanded))
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
	private bool CheckIsExpanded(ControlHierarchicalViewModel node, List<ControlHierarchicalViewModel> nodesExpanded)
	{ 
		// Recorre la colección
		foreach (ControlHierarchicalViewModel nodeExpanded in nodesExpanded)
			if (nodeExpanded is ControlHierarchicalViewModel nodeFirst && node is ControlHierarchicalViewModel nodeSecond)
			{
				if (nodeFirst.IsEquals(nodeSecond))
					return true;
			}
		// Si ha llegado hasta aquí es porque no ha encontrado nada
		return false;
	}

	/// <summary>
	///		Obtiene el tipo de nodo seleccionado
	/// </summary>
	protected string GetSelectedNodeType() => SelectedNode?.Type ?? string.Empty;

	/// <summary>
	///		Contexto de sincronización
	/// </summary>
	public SynchronizationContext ContextUI { get; }

	/// <summary>
	///		Nodos
	/// </summary>
	public AsyncObservableCollection<ControlHierarchicalViewModel> Children
	{
		get { return _children; }
		set { CheckObject(ref _children, value); }
	}

	/// <summary>
	///		Nodo seleccionado
	/// </summary>
	public virtual ControlHierarchicalViewModel? SelectedNode
	{
		get { return _selectedNode; }
		set { CheckObject(ref _selectedNode, value); }
	}
}
