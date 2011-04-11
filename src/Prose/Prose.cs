// Prose.cs
using System;
using System.Windows;
using System.Windows.Controls;

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
        public ProseApplication()
        {
        }

        void BuildControls(Window window)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            var textbox = new TextBox { AcceptsReturn = true, AcceptsTab = true };
            Grid.SetColumn(textbox, 0);
            Grid.SetRow(textbox, 0);
            grid.Children.Add(textbox);
            window.Content = grid;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);

             var window = new Window();
             BuildControls(window);
             window.Title = "Prose - because code should be beautiful...";
             window.Show();
        }
    }
}