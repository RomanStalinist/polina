using System.Windows;
using polina.classes;

namespace polina;

public partial class InWindow
{
    public InWindow()
    {
        InitializeComponent();
    }

    private void enterButton_OnClick(object sender, RoutedEventArgs e)
    {
        string phone = TextBox.Text, password = PasswordBox.Password;
        if (phone.Length == 0 || password.Length == 0)
        {
            h.showError("Введите логин и пароль",
                "Ошибка авторизации");
        }
        else
        {
            try
            {
                App.User = App.AppDbContext.users.First(u => u.phone == phone && u.password == password);
                navigator.show<MainWindow>();
                navigator.close<InWindow>();
            }
            catch (Exception ex)
            {
                h.showError("Неверный логин или пароль",
                    "Ошибка авторизации");
                h.consoleLog(ex);
                h.debug(ex);
            }
        }
    }

    private void registerButton_OnClick(object sender, RoutedEventArgs e)
    {
        new UpWindow().Show();
        Close();
    }
}