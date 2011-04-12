// Prose.cs
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Prose
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
			System.Console.WriteLine("tesT");
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
            editor = new TextBox
                         { 
                             AcceptsReturn = true, 
                             AcceptsTab = true, 
                             Background = Brushes.Black, 
                             Foreground = Brushes.Lime,
                             FontFamily = new FontFamily("Consolas"),
                             FontSize = 16.0,
                             TabIndex = 0
                         };
			editor.KeyUp += EditorOnKeyUp;
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

        void WriteFile()
        {
            string outFileName;

            if (!string.IsNullOrEmpty(File))
                outFileName = File;
            else
            {
                var currentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                outFileName = Path.Combine(currentFolder ,"out.cs");
            }

            System.IO.File.WriteAllText(outFileName, editor.Text);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
             base.OnStartup(e);

             var window = new Window();
             BuildControls(window);
             SetContent();
             window.Title = "Prose - because code should be beautiful...";
             window.KeyDown += WindowOnKeyDown;
             window.Show();
        }

        void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S))
                WriteFile();
        }
		
		void EditorOnKeyUp(object sender, KeyEventArgs e)
		{
			//MessageBox.Show(e.Key.ToString());
			//MessageBox.Show(string.Format("{0} len {1}", editor.SelectionStart, editor.SelectionLength));
			//MessageBox.Show(string.Format("Selected Text: '{0}'", editor.SelectedText));

			if (e.Key == Key.Tab)
				HandleTab();
			if (e.Key == Key.Enter)
				HandleEnter();
		}
		
		void HandleTab()
		{
			var oldSelectionStart = editor.SelectionStart;
			editor.SelectionStart -= 1;
			editor.SelectionLength = 1;
			editor.SelectedText = @"    ";
			editor.SelectionStart = oldSelectionStart + 3;
			editor.SelectionLength = 0;
		}
		
		void HandleEnter()
		{
			//MessageBox.Show(editor.Text.LastIndexOf("\r\n",editor.SelectionStart).ToString());
		}
    }
}