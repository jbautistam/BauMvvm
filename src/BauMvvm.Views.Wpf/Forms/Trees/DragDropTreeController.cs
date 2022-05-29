using System;
using System.Windows;
using System.Windows.Controls;

using Bau.Libraries.BauMvvm.ViewModels.Forms.ControlItems;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Forms.Trees
{
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
		public void InitDragOperation(TreeView tree, IHierarchicalViewModel node)
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
		public IHierarchicalViewModel GetDragDropFileNode(IDataObject dataObject)
		{
			IHierarchicalViewModel node = null;

				// Obtiene los datos que se están arrastrando
				if (dataObject.GetDataPresent(KeyDataObject))
					node = dataObject.GetData(KeyDataObject) as IHierarchicalViewModel;
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
}

/*
private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
    this._startPoint = e.GetPosition(null);
}

private void Window_MouseMove(object sender, MouseEventArgs e)
{
    //drag is heppen
    //Prepare for Drag and Drop
    Point mpos = e.GetPosition(null);
    Vector diff = this._startPoint - mpos;

    if (e.LeftButton == MouseButtonState.Pressed &&
        (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
    {

        //hooking on Mouse Up
        InterceptMouse.m_hookID = InterceptMouse.SetHook(InterceptMouse.m_proc);

        //ataching the event for hadling drop
        this.QueryContinueDrag += queryhandler;
        //begin drag and drop
        DataObject dataObj = new DataObject(this.text1);
        DragDrop.DoDragDrop(this.text1, dataObj, DragDropEffects.Move);

    }
}
*/