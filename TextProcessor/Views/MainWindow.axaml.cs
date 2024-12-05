using Avalonia.Controls;
using Avalonia.Media;
using System.Linq;

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

            if (result != null && result.Length > 0)
            {
                var fileContent = System.IO.File.ReadAllLines(result[0]);

                if (fileContent.Length > 0)
                {
                    var firstLine = fileContent[0].Split(':');
                    if (firstLine.Length == 3)
                    {
                        Editor.FontFamily = new FontFamily(firstLine[0]);

                        if (double.TryParse(firstLine[1], out double fontSize))
                        {
                            Editor.FontSize = fontSize;
                        }

                        if (Color.TryParse(firstLine[2], out Color color))
                        {
                            Editor.Foreground = new SolidColorBrush(color);
                        }
                    }

                    Editor.Text = string.Join("\n", fileContent.Skip(1));
                }
            }

            UndoButton.IsVisible = true;
        }

        private async void SaveFile(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                var fontFamily = Editor.FontFamily?.ToString() ?? "Default";
                var fontSize = Editor.FontSize.ToString();
                var fontColor = Editor.Foreground is SolidColorBrush brush ? brush.Color.ToString() : "#000000";

                var fileContent = $"{fontFamily}:{fontSize}:{fontColor}\n{Editor.Text}";

                System.IO.File.WriteAllText(result, fileContent);
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

        private async void SetFont(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var fontDialog = new FontPickerDialog();
            var selectedFont = await fontDialog.ShowStringDialog(this);
            if (selectedFont != null)
            {
                Editor.FontFamily = new FontFamily(selectedFont);
            }
        }

        private async void SetFontSize(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var inputDialog = new InputDialog("Enter font size", "Font size:");
            var result = await inputDialog.ShowStringDialog(this);
            if (double.TryParse(result, out double fontSize))
            {
                Editor.FontSize = fontSize;
            }
        }

        private async void SetFontColor(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var colorDialog = new ColorPickerDialog();
            var color = await colorDialog.ShowStringDialog(this);
            if (color.HasValue)
            {
                Editor.Foreground = new SolidColorBrush(color.Value);
            }
        }
    }
}