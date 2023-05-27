namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers;

/// <summary>
///		Controlador de mensajería principal
/// </summary>
public class HostMessengerController : ViewModels.Controllers.IHostMessengerController
{
	public HostMessengerController(HostController hostController)
	{
		HostController = hostController;
	}

    /// <summary>
    ///     Suscribe una acción a un evento
    /// </summary>
	public void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender? source = null) where TSender : class
	{
		throw new NotImplementedException();
	}

    /// <summary>
    ///     Envía un evento
    /// </summary>
	public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
	{
		throw new NotImplementedException();
	}

	/// <summary>
	///		Controlador principal
	/// </summary>
	public HostController HostController { get; }
}
