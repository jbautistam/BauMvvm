﻿using System.Collections.Specialized;
using System.ComponentModel;

namespace Bau.Libraries.BauMvvm.ViewModels;

/// <summary>
///     Colección asíncrona de objetos observables
/// </summary>
public class AsyncObservableCollection<TypeData> : System.Collections.ObjectModel.ObservableCollection<TypeData>
{
    // Variables privadas
    private static SynchronizationContext? _synchronizationContext = SynchronizationContext.Current;

    public AsyncObservableCollection() {}

    public AsyncObservableCollection(IEnumerable<TypeData> list) : base(list) {}

    /// <summary>
    ///     Cuando se modifica la colección, si estamos en el mismo contexto, lanzamos el evento base, si no, saltamos de contexto
    /// y lanzamos el evento base
    /// </summary>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (_synchronizationContext is null || SynchronizationContext.Current == _synchronizationContext)
            RaiseCollectionChanged(e);
        else
            _synchronizationContext.Send(RaiseCollectionChanged, e);
    }

    /// <summary>
    ///     Lanza el evento de colección modificada: cuando estamos en el mismo hilo que en la creación simplemente llamamos a la implementación base
    /// </summary>
    private void RaiseCollectionChanged(object? param)
    {
        if (param is NotifyCollectionChangedEventArgs parameter)
            base.OnCollectionChanged(parameter);
    }

    /// <summary>
    ///     Cuando se modifica, dependiendo de si estamos o no en el contexto, lanzamos el evento OnPropertyChanged
    /// o lo lanzamos sobre el contexto de sincronización inicial
    /// </summary>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (_synchronizationContext is null || SynchronizationContext.Current == _synchronizationContext)
            RaisePropertyChanged(e);
        else
            _synchronizationContext.Send(RaisePropertyChanged, e);
    }

    /// <summary>
    ///     Cuando estamos en el hilo de creación, llamamos a la implementación base de OnPropertyChanged directamente
    /// </summary>
    private void RaisePropertyChanged(object? param)
    {
        if (param is PropertyChangedEventArgs parameter)
            base.OnPropertyChanged(parameter);
    }

    /// <summary>
    ///     Añade una serie de elementos a la colección
    /// </summary>
	public void AddRange(List<TypeData> items)
	{
		foreach (TypeData item in items)
            Add(item);
	}
}
