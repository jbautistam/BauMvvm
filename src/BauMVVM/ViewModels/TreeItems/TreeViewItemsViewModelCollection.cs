using System;
using System.Collections.ObjectModel;

namespace Bau.Libraries.MVVM.ViewModels.TreeItems
{
	/// <summary>
	///		Colección de <see cref="TreeViewItemViewModel"/>
	/// </summary>
	public class TreeViewItemsViewModelCollection : ObservableCollection<ITreeViewItemViewModel>
	{
		/// <summary>
		///		Añade una serie de elementos
		/// </summary>
		public void AddRange(ObservableCollection<ITreeViewItemViewModel> items)
		{
			foreach (ITreeViewItemViewModel item in items)
				Add(item);
		}

		/// <summary>
		///		Depuración
		/// </summary>
		public void Debug()
		{
			Debug(this, 0);
		}

		/// <summary>
		///		Depuración
		/// </summary>
		private void Debug(ObservableCollection<ITreeViewItemViewModel> nodes, int indent)
		{
			foreach (TreeViewItemViewModel node in nodes)
			{ 
				// Indenta la cadena
				for (int index = 0; index < indent; index++)
					System.Diagnostics.Debug.Write("  ");
				// Escribe el elemento
				System.Diagnostics.Debug.WriteLine(node.Text);
				// Escribe los hijos
				if (node.Children != null && node.Children.Count > 0)
					Debug(node.Children, indent + 1);
			}
		}
	}
}
