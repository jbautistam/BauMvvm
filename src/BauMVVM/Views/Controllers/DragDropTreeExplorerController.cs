using System;
using System.Windows;
using System.Windows.Controls;

namespace Bau.Libraries.MVVM.Views.Controllers
{
	/// <summary>
	///		Controlador de dragDrop de un árbol
	/// </summary>
	public class DragDropTreeExplorerController
	{
		public DragDropTreeExplorerController(string keyDataObject = "BaseNodeViewModel")
		{
			KeyDataObject = keyDataObject;
		}

		/// <summary>
		///		Inicia la operación de Drag & Drop
		/// </summary>
		public void InitDragOperation(TreeView tree, ViewModels.TreeItems.ITreeViewItemViewModel node)
		{
			if (node != null)
				DragDrop.DoDragDrop(tree, new DataObject(KeyDataObject, node), DragDropEffects.Move);
		}

		/// <summary>
		///		Obtiene el nodo de archivo que se está arrastrando
		/// </summary>
		public ViewModels.TreeItems.ITreeViewItemViewModel GetDragDropFileNode(IDataObject dataObject)
		{
			ViewModels.TreeItems.ITreeViewItemViewModel node = null;

				// Obtiene los datos que se están arrastrando
				if (dataObject.GetDataPresent(KeyDataObject))
					node = dataObject.GetData(KeyDataObject) as ViewModels.TreeItems.ITreeViewItemViewModel;
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
		///		Clave del objeto utilizado para almacenar los datos a arrastrar y copiar
		/// </summary>
		public string KeyDataObject { get; }
	}
}
