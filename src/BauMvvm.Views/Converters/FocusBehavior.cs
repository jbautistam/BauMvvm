﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bau.Libraries.BauMvvm.Views.Converters;

/// <summary>
///		Comportamiento para pasar el foco al primer elemento
/// </summary>
public static class FocusBehavior
{
    public static readonly DependencyProperty FocusFirstProperty = DependencyProperty.RegisterAttached("FocusFirst", typeof(bool), typeof(FocusBehavior),
																									   new PropertyMetadata(false, OnFocusFirstPropertyChanged));

	/// <summary>
	///		Obtiene el valor de la propiedad
	/// </summary>
    public static bool GetFocusFirst(Control control)
    {
        return (bool) control.GetValue(FocusFirstProperty);
    }

	/// <summary>
	///		Asigna el valor de la propiedad
	/// </summary>
    public static void SetFocusFirst(Control control, bool value)
    {
        control.SetValue(FocusFirstProperty, value);
    }

    static void OnFocusFirstPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        if (obj is Control control)
        {
			if (((args.NewValue as bool?) ?? false))
				control.Loaded += (sender, e) => control.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
