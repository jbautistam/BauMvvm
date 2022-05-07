using System;
using System.Windows;
using System.Windows.Controls;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Forms.Dialogs
{
	/// <summary>
	///		Ventana para mostrar una contraseña
	/// </summary>
	public partial class InputPasswordBoxView : Window
	{
		public InputPasswordBoxView(Controllers.HostSystemController controller, string message, string inputText)
		{ 
			// Inicializa los componentes
			InitializeComponent();
			// Inicializa las propiedades
			Controller = controller;
			Message = message;
			Password = inputText;
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{
			lblMessage.Text = Message;
			txtPassword.Password = Password;
		}

		/// <summary>
		///		Comprueba los datos introducidos
		/// </summary>
		private bool ValidateData()
		{
			bool validate = false;

				// Comprueba los datos
				if (string.IsNullOrWhiteSpace(txtPassword.Password))
					Controller.ShowMessage("Introduzca la contraseña");
				else
					validate = true;
				// Devuelve el valor que indica si los datos son correctos
				return validate;
		}

		/// <summary>
		///		Graba los datos
		/// </summary>
		private void Save()
		{
			if (ValidateData())
			{ 
				// Asigna el texto
				Password = txtPassword.Password;
				// Cierra el formulario
				DialogResult = true;
				Close();
			}
		}

		/// <summary>
		///		Controlador principal
		/// </summary>
		public Controllers.HostSystemController Controller { get; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		///		Contraseña introducida por el usuario
		/// </summary>
		public string Password { get; set; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitForm();
		}

		private void cmdSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{	
			Password = (sender as PasswordBox)?.Password;
		}
	}
}
