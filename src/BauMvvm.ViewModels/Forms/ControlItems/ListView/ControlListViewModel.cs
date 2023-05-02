namespace Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.ListView;

/// <summary>
///		ViewModel para control con una lista de elementos
/// </summary>
public class ControlListViewModel : BaseObservableObject
{
    // Variables privadas
    private ControlItemViewModel? _selectedItem;

    public ControlListViewModel()
    {
        ContextUI = SynchronizationContext.Current;
    }

    /// <summary>
    ///		Añade un elemento
    /// </summary>
    public void Add(ControlItemViewModel item, bool selected)
    {
        //TODO Eliminar cuando comprobemos que realmente el AsyncObservableCollection no precisa ContextUI
        #if DEBUG
            // Añade el elemento
            Items.Add(item);
            // Indica si está seleccionado
            if (selected)
            {
                item.IsSelected = true;
                SelectedItem = item;
            }
        #else
        object state = new object();

            // Carga los nodos en el árbol
            //? _contexUi mantiene el contexto de sincronización que creó el ViewModel (que debería ser la interface de usuario)
            //? Si se generasen elementos en otro hilo o desde un evento, se obtendría un error de tipo
            //? excepción del tipo "Este tipo de CollectionView no admite cambios en su SourceCollection desde un hilo diferente del hilo Dispatcher"
            //? Por eso se tiene que añadir desde el contexto de sincronización de la UI
            ContextUI?.Send(_ => {
                                    // Añade el elemento
                                    Items.Add(item);
                                    // Indica si está seleccionado
                                    if (selected)
                                    {
                                        item.IsSelected = true;
                                        SelectedItem = item;
                                    }
                                },
                                state
                            );
        #endif
    }

    /// <summary>
    ///		Contexto de sincronización
    /// </summary>
    internal SynchronizationContext? ContextUI { get; }

    /// <summary>
    ///		Elementos
    /// </summary>
    public AsyncObservableCollection<ControlItemViewModel> Items { get; } = new();

    /// <summary>
    ///		Elemento seleccionado
    /// </summary>
    public ControlItemViewModel? SelectedItem
    {
        get { return _selectedItem; }
        set { CheckObject(ref _selectedItem, value); }
    }
}
