using System;

namespace Bau.Libraries.MVVM.ViewModels.Menus
{
	/// <summary>
	///		ViewModel para un menú
	/// </summary>
	public class MenuItemViewModel
	{
		public MenuItemViewModel(string text = null, string icon = null, BaseCommand command = null)
		{
			Text = text;
			Icon = icon;
			Command = command;
		}

		/// <summary>
		///		Texto
		/// </summary>
		public string Text { get; }

		/// <summary>
		///		Icono del menú
		/// </summary>
		public string Icon { get; }

		/// <summary>
		///		Comando
		/// </summary>
		public BaseCommand Command { get; }

		/// <summary>
		///		Indica si el menú es un separador
		/// </summary>
		public bool IsSeparator
		{
			get { return string.IsNullOrEmpty(Text); }
		}
	}
}
