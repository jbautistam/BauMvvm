using Bau.Libraries.BauMvvm.ViewModels.Controllers;

namespace Bau.Libraries.BauMvvm.Views.Wpf.Controllers.Notifications;

/// <summary>
///		Manager de notificaciones tipo Toast
/// </summary>
internal class NotificationsManager
{
    // Constantes privadas
    private const int MaxLengthTitle = 1_000;
    private const int MaxLengthBody = 1_000;

    /// <summary>
    ///     Muestra una notificación
    /// </summary>
    internal void ShowNotification(SystemControllerEnums.NotificationType type, string applicationName, 
                                   string title, string message, TimeSpan expiration, string? urlImage = null)
    {
        //Windows.Data.Xml.Dom.XmlDocument xmlToast = new System.Windows.Data.Xml.Dom.XmlDocument();

        //    // Carga el XML
        //    xmlToast.LoadXml(GetTemplate(title, message, GetUrlImage(type, urlImage)));
        //    // Muestra la notificación
        //    ShowNotification(xmlToast, applicationName);
    }

    ///// <summary>
    /////     Muestra una notificación
    ///// </summary>
    //private void ShowNotification(Windows.Data.Xml.Dom.XmlDocument xmlToast, string applicationName)
    //{
    //    ToastNotification toast = new ToastNotification(xmlToast);

    //        // Muestra la notificación
    //        ToastNotificationManager.CreateToastNotifier(applicationName).Show(toast);
    //}

    /// <summary>
    ///     Obtiene la plantilla para la notificación
    /// </summary>
    private string GetTemplate(string title, string message, string urlImage)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

            // Genera la plantilla
            builder.AppendLine("<?xml version='1.0'?>");
            builder.AppendLine("<toast>");
            builder.AppendLine("<visual>");
            builder.AppendLine($"<binding template='{GetTemplateType(urlImage)}'>");
            // Añade los elementos
            if (!string.IsNullOrWhiteSpace(urlImage))
                builder.AppendLine($"<image id='1' src='{urlImage}'/>");
            builder.AppendLine($"<text id='1'>{Normalize(title, MaxLengthTitle)}</text>");
            builder.AppendLine($"<text id='2'>{Normalize(message, MaxLengthBody)}</text>");
            // Cierra la plantilla
            builder.AppendLine("</binding>");
            builder.AppendLine("</visual>");
            builder.AppendLine("</toast>");
            // Devuelve la plantilla
            return builder.ToString();
    }

    /// <summary>
    ///     Normaliza el texto para que se pueda introducir en una cadena XML
    /// </summary>
    private string Normalize(string text, int maxLength)
    {
        // Reemplaza los contenidos "raros"
        if (!string.IsNullOrWhiteSpace(text))
        {
            // Quita los caracteres extraños
            //text = text.Replace("&", "&amp;");
            //text = text.Replace("<", "&lt;");
            //text = text.Replace(">", "&gt;");
            //text = text.Replace("\"", "&quot;");
            //text = text.Replace("'", "&apos;");
            // Corta la cadena
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            // Añade un CData
            text = $"<![CDATA[{text}]]>";
        }
        // Devuelve el texto
        return text;
    }

    /// <summary>
    ///     Obtiene el tipo de plantilla
    /// </summary>
    private string GetTemplateType(string urlImage)
    {
        if (!string.IsNullOrWhiteSpace(urlImage))
            return "ToastImageAndText02";
        else
            return "ToastText02";
    }

    /// <summary>
    ///     Obtiene la URL de la imagen dependiendo del tipo
    /// </summary>
    private string GetUrlImage(SystemControllerEnums.NotificationType type, string urlImage)
    {
        return string.Empty;
        /*
        if (!string.IsNullOrWhiteSpace(urlImage))
            return urlImage;
        else
            switch (type)
            {
                case SystemControllerEnums.NotificationType.Error:
                    return "ms-appx:///Themes/Images/Error.png";
                case SystemControllerEnums.NotificationType.Warning:
                    return "ms-appx:///Themes/Images/Warning.png";
                case SystemControllerEnums.NotificationType.Other:
                    return "ms-appx:///Themes/Images/Info.png";
                default:
                    return "ms-appx:///Themes/Images/Info.png";
            }
        */
    }
}
