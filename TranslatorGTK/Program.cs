using System;
using Gtk;

namespace TranslatorGTK
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            win.Decorated = true;
            win.Modal = true;
            win.KeepAbove = true;
            Application.Run();
        }
    }
}
