// Prose.cs
using System;
using System.Windows;

namespace Prose
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main()
        {
            var app = new ProseApplication();
            app.Run();
        }
    }

    public class ProseApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);

             var window = new Window();
             window.Title = "Prose - because code should be beautiful...";
             window.Show();
        }
    }
}