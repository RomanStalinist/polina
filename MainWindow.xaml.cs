using polina.classes;

namespace polina;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Frame.Content = new BuyPage();
        AccountBlock.Text = App.User.name;
        if (App.User.role == "A")
        {
            AccountBlock.Text = "🍌 " + AccountBlock.Text;
        }
    }

    private void navigateOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is System.Windows.Controls.TextBlock t)
        {
            switch (t.Name)
            {
                case "BuyBtn":
                    Frame.Content = new BuyPage();
                    break;
                case "SellBtn":
                    Frame.Content = new SellPage();
                    break;
                case "BackupBtn":
                    Frame.Content = new BackupPage();
                    break;
                default:
                    App.User = null!;
                    navigator.show<InWindow>();
                    navigator.close<MainWindow>();
                    break;
            }
        }
    }
}