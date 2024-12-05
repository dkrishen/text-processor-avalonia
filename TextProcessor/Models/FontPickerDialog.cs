using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FontPickerDialog : Window
{
    private string? _selectedFont;

    public FontPickerDialog()
    {
        this.Title = "Fonts";
        var stackPanel = new StackPanel();

        var fontList = new ListBox();
        fontList.ItemsSource = new List<string> { "Arial", "Courier New", "Times New Roman", "Verdana" };

        var okButton = new Button { Content = "OK" };
        okButton.Click += (_, __) =>
        {
            _selectedFont = fontList.SelectedItem?.ToString();
            Close();
        };

        stackPanel.Children.Add(new TextBlock { Text = "select a font:" });
        stackPanel.Children.Add(fontList);
        stackPanel.Children.Add(okButton);

        this.Content = stackPanel;
        this.Width = 300;
        this.Height = 200;
    }

    public async Task<string?> ShowStringDialog(Window parent)
    {
        await this.ShowDialog(parent);
        return _selectedFont;
    }
}