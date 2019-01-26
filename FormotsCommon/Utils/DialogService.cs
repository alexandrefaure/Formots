using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsCommon.Utils
{
    public class DialogService
    {
        public async Task<MessageDialogResult> ShowMessage(string windowTitle, string message,
            MessageDialogStyle dialogStyle, MetroDialogSettings metroDialogSettings)
        {
            foreach (var window in Application.Current.Windows.OfType<MetroWindow>())
                if (window.Name == "FormotsMainWindow")
                {
                    window.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
                    return await window.ShowMessageAsync(windowTitle, message, dialogStyle, metroDialogSettings);
                }
            return MessageDialogResult.Negative;
        }

        public async Task<ProgressDialogController> ShowProgressAsync(string header, string message, MetroDialogSettings metroDialogSettings)
        {
            foreach (var window in Application.Current.Windows.OfType<MetroWindow>())
                if (window.Name == "FormotsMainWindow")
                {
                    window.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
                    return await window.ShowProgressAsync(header, message, false, metroDialogSettings);
                }
            return null;
        }
    }
}