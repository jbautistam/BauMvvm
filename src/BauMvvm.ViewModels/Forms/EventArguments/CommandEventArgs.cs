namespace Bau.Libraries.BauMvvm.ViewModels.Forms.EventArguments;

/// <summary>
///		Argumentos del evento de ejecución de un comando
/// </summary>
public class CommandEventArgs<TypeData> : EventArgs
{
	public CommandEventArgs(string command, TypeData item)
	{
		Command = command;
		Item = item;
	}

	/// <summary>
	///		Nombre del comando
	/// </summary>
	public string Command { get; }

	/// <summary>
	///		Elemento relacionado con el comando
	/// </summary>
	public TypeData Item { get; }
}
