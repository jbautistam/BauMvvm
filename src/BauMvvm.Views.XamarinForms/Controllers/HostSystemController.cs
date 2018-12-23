using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.XamarinForms.Controllers
{
    /// <summary>
    ///     Controlador de ventanas
    /// </summary>
    public class HostSystemController : IHostSystemControllerAsync
    {
		public HostSystemController(Application app, string appTitle)
		{
			App = app;
			AppTitle = appTitle;
		}

		/// <summary>
		///		Muestra una ventana con un mensaje
		/// </summary>
		public async Task ShowMessageAsync(string message)
        {
			await App.MainPage.DisplayAlert(AppTitle, message, "OK");
        }

		/// <summary>
		///		Muestra una ventana con una pregunta
		/// </summary>
		public async Task<bool> ShowQuestionAsync(string message, string acceptTitle = "Aceptar", string cancelTitle = "Cancelar")
        {
			return await App.MainPage.DisplayAlert(AppTitle, message, acceptTitle, cancelTitle);
        }

		/// <summary>
		///		Muestra una notificación
		/// </summary>
		public async Task ShowNotificationAsync(SystemControllerEnums.NotificationType type, string title, string message, TimeSpan expiration, string urlImage = null)
		{
			// UserDialogs.Instance.Toast("Toast message: <3", TimeSpan.FromMilliseconds(millis));
			await ShowMessageAsync(message);
		}

        /// <summary>
        ///     Abre una página con una web
        /// </summary>
        public void OpenUrl(Uri uri)
        {
            Device.OpenUri(uri);
        }

        /// <summary>
        ///		Muestra una pregunta con tres posibles respuestas
        /// </summary>
        public async Task<SystemControllerEnums.ResultType> ShowQuestionCancelAsync(string message)
        {
            return SystemControllerEnums.ResultType.No;
        }

		/// <summary>
		///		Muestra un cuadro de diálogo para introducir un texto
		/// </summary>
		public async Task<(SystemControllerEnums.ResultType result, string input)> ShowInputStringAsync(string message, string defaultValue = null)
        {
            return (SystemControllerEnums.ResultType.No, string.Empty);
        }

		/// <summary>
		///		Aplicación principal
		/// </summary>
		public Application App { get; }

		/// <summary>
		///		Título de la aplicación
		/// </summary>
		public string AppTitle { get; }
	}
}
