using System;
using MahApps.Metro.Controls;

namespace FormotsCommon.Utils
{
    public static class WindowHelper
    {
        public static T ShowDialogWindow<T>() where T : MetroWindow
        {
            var window = (T)Activator.CreateInstance(typeof(T));
            //window.Topmost = true; //Pour toujours afficher la fenêtre au-dessus de toutes les autres dans Windows
            window.ShowDialog(); //ShowDialog pour que le code attende la fermeture de cette fenêtre
            return window;
        }
    }
}
