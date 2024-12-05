using Avalonia.Controls;
using System.Threading.Tasks;

public class InputDialog : Window
{
    private string? _result;

    public InputDialog(string title, string message)
    {
        this.Title = title;
        var stackPanel = new StackPanel();
        var textBox = new TextBox { Width = 200 };
        var okButton = new Button { Content = "OK" };

        okButton.Click += (_, __) =>
        {
            _result = textBox.Text;
            Close();
        };

        stackPanel.Children.Add(new TextBlock { Text = message });
        stackPanel.Children.Add(textBox);
        stackPanel.Children.Add(okButton);

        this.Content = stackPanel;
        this.Width = 300;
        this.Height = 200;
    }

    public async Task<string?> ShowStringDialog(Window parent)
    {
        await this.ShowDialog(parent);
        return _result;
    }
}