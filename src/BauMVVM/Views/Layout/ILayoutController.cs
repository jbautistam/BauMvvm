using System;
using System.Windows.Controls;

namespace Bau.Libraries.MVVM.Views.Layout
{
	/// <summary>
	///		Interface para los controladores de layout
	/// </summary>
	public interface ILayoutController
	{
		/// <summary>
		///		Muestra un panel en uno de los paneles de la ventana principal
		/// </summary>
		void ShowDockPane(string windowID, LayoutEnums.DockPosition position, string title, UserControl innerControl,
						  ViewModels.IViewModel viewModel);

		/// <summary>
		///		Muestra un documento
		/// </summary>
		void ShowDocument(string windowID, string title, UserControl document,
						  ViewModels.Forms.Interfaces.IFormViewModel viewModel = null);

		/// <summary>
		///		Muestra una ventana de edición de código
		/// </summary>
		void ShowCodeEditor(string fileName, string template, LayoutEnums.Editor editor,
							string fileNameHelp = null);

		/// <summary>
		///		Muestra el navegador Web de una URL
		/// </summary>
		void ShowWebBrowser(string url);

		/// <summary>
		///		Muestra una imagen a partir de un nombre de archivo
		/// </summary>
		void ShowImage(string fileName);

		/// <summary>
		///		Muestra un archivo de texto a partir de un nombre de archivo
		/// </summary>
		void ShowTextFile(string fileName);

		/// <summary>
		///		Graba todos los documentos
		/// </summary>
		void SaveAllDocuments();
	}
}
