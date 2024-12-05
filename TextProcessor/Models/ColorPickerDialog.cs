using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ColorPickerDialog : Window
{
    private Color? _selectedColor;

    public ColorPickerDialog()
    {
        this.Title = "Color selection";
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10)
        };

        var colors = new List<Color>
        {
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Yellow,
            Colors.Cyan,
            Colors.Magenta,
            Colors.Black,
            Colors.White,
            Colors.Gray
        };

        foreach (var color in colors)
        {
            var colorButton = new Button
            {
                Background = new SolidColorBrush(color),
                Width = 50,
                Height = 50,
                Margin = new Thickness(2)
            };

            colorButton.Click += (sender, e) =>
            {
                _selectedColor = color;
                Close();
            };

            stackPanel.Children.Add(colorButton);
        }

        var cancelButton = new Button
        {
            Content = "Cancel",
            Margin = new Thickness(0, 10, 0, 0)
        };
        cancelButton.Click += (_, __) => Close();

        stackPanel.Children.Add(cancelButton);

        this.Content = stackPanel;
        this.Width = 200;
        this.Height = 300;
    }

    public async Task<Color?> ShowStringDialog(Window parent)
    {
        await this.ShowDialog(parent);
        return _selectedColor;
    }
}