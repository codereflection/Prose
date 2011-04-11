// Prose.cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Prose
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new ProseApplication();

            if (args.Length > 0)
                app.File = args[0];

            app.Run();
        }
    }

    public class ProseApplication : Application
    {
        public ProseApplication()
        {
        }

        public string File { get; set; }

        TextBox editor;

        void BuildControls(Window window)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            editor = new TextBox { 
                                              AcceptsReturn = true, 
                                              AcceptsTab = true, 
                                              Background = Brushes.Black, 
                                              Foreground = Brushes.Lime,
                                              FontFamily = new FontFamily("Consolas"),
                                              FontSize = 16.0,
                                              TabIndex = 0
                                              };
            Grid.SetColumn(editor, 0);
            Grid.SetRow(editor, 0);
            grid.Children.Add(editor);
            window.Content = grid;
            editor.Focus();
        }

        void SetContent()
        {
            if (string.IsNullOrEmpty(File) || !System.IO.File.Exists(File))
                return;

            var content = System.IO.File.ReadAllText(File);
            editor.Text = content;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);

             var window = new Window();
             BuildControls(window);
             SetContent();
             window.Title = "Prose - because code should be beautiful...";
             window.Show();
        }
    }
}