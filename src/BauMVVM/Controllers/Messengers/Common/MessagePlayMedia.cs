using System;
using System.Collections.Generic;

namespace Bau.Libraries.MVVM.Controllers.Messengers.Common
{
	/// <summary>
	///		Mesaje para añadir a la cola la reproducción de un archivo
	/// </summary>
	public class MessagePlayMedia: Message
	{ 
		public MessagePlayMedia(string source, string group, List<KeyValuePair<string, string>> files, object content)
							: base(source, "PLAY", "Play", content)
		{
			Group = group;
			Files = files;
		}

		/// <summary>
		///		Grupo al que pertenecen los archivos
		/// </summary>
		public string Group { get; }

		/// <summary>
		///		Archivos
		/// </summary>
		public List<KeyValuePair<string, string>> Files { get; }
	}
}
