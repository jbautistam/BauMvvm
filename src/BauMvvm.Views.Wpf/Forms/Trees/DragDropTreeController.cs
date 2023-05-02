using System;
using System.Windows;
using System.Windows.Controls;

using Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems.Trees;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Forms.Trees;

/// <summary>
///		Controlador de dragDrop de un árbol
/// </summary>
public class DragDropTreeController
{
	public DragDropTreeController(Control source, string keyDataObject, bool hookExternalWindows = false)
	{
		Source = source;
		KeyDataObject = keyDataObject;
		HookExternalWindows = hookExternalWindows;
	}

	/// <summary>
	///		Inicia la operación de Drag & Drop
	/// </summary>
	public void InitDragOperation(TreeView tree, ControlHierarchicalViewModel node)
	{
		if (node != null)
		{
			// Si se tienen que controlar los drop sobre aplicaciones externas, asigna los manejadores
			if (HookExternalWindows)
			{
				// Asocia los mensajes de Windows de MouseUp
				//InterceptMouse.m_hookID = InterceptMouse.SetHook(InterceptMouse.m_proc);
				// Asocia el evento que maneja el drop
				Source.QueryContinueDrag += Source_QueryContinueDrag;
			}
			// Inicializa la operación de Drag & Drop
			DragDrop.DoDragDrop(tree, new DataObject(KeyDataObject, node), DragDropEffects.Move);
		}
	}

	/// <summary>
	///		Obtiene el nodo de archivo que se está arrastrando
	/// </summary>
	public ControlHierarchicalViewModel GetDragDropFileNode(IDataObject dataObject)
	{
		ControlHierarchicalViewModel node = null;

			// Obtiene los datos que se están arrastrando
			if (dataObject.GetDataPresent(KeyDataObject))
				node = dataObject.GetData(KeyDataObject) as ControlHierarchicalViewModel;
			// Devuelve los datos del nodo que se está arrastrando
			return node;
	}

	/// <summary>
	///		Trata el evento de entrada en un control
	/// </summary>
	public void TreatDragEnter(DragEventArgs e)
	{
		if (GetDragDropFileNode(e.Data) != null)
			e.Effects = DragDropEffects.Copy;
		else
			e.Effects = DragDropEffects.None;
	}

	/// <summary>
	///		Maneja el evento de drag continuo sobre las aplicaciones externas
	/// </summary>
	private void Source_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
	{
		//if (e.Action == DragAction.Drop)
		//	System.Diagnostics.Debug.WriteLine("Drop");
		// throw new NotImplementedException();
	}

	/// <summary>
	///		Control que origina el manejador
	/// </summary>
	public Control Source { get; }

	/// <summary>
	///		Clave del objeto utilizado para almacenar los datos a arrastrar y copiar
	/// </summary>
	public string KeyDataObject { get; }

	/// <summary>
	///		Indica si se debe controlar cuando se suelta el nodo sobre una aplicación externa
	/// </summary>
	public bool HookExternalWindows { get; }
}