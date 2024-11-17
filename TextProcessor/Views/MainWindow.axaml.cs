using Avalonia.Controls;
using System.Linq;
using System.Threading.Tasks;

namespace TextProcessor.Views
{
    public partial class MainWindow : Window
    {
        string CopiedText = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            RedoButton.IsVisible = false;
            UndoButton.IsVisible = false;
        }

        private async void OpenFile(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                var text = System.IO.File.ReadAllText(result[0]);
                Editor.Text = text;
            }

            UndoButton.IsVisible = true;
        }

        private async void SaveFile(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                System.IO.File.WriteAllText(result, Editor.Text);
            }
        }

        private void CopyText(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            CopiedText = Editor.Text;
        }

        private void PasteText(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Editor.Text += CopiedText;
            UndoButton.IsVisible = true;
        }

        private void Cut(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            CopiedText = Editor.Text;
            Editor.Text = string.Empty;
            UndoButton.IsVisible = true;
        }

        private void Undo(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Editor.Undo();
            RedoButton.IsVisible = Editor.CanRedo;
            UndoButton.IsVisible = Editor.CanUndo;
        }

        private void Redo(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Editor.Redo();
            UndoButton.IsVisible = Editor.CanUndo;
            RedoButton.IsVisible = Editor.CanRedo;
        }

        private void Clear(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Editor.Text = string.Empty;
            UndoButton.IsVisible = true;
        }

        private void ChangeCase(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Editor.Text))
            {
                var text = Editor.Text;
                var isUpper = text == text.ToUpper();
                Editor.Text = isUpper ? text.ToLower() : text.ToUpper();
                
                UndoButton.IsVisible = true;
            }
        }
    }
}