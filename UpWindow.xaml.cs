using polina.classes;

namespace polina;

public partial class UpWindow
{
    public UpWindow()
    {
        InitializeComponent();
    }

    private void registerButton_OnClick(object sender, System.Windows.RoutedEventArgs e)
    {
        string name = NameTextBox.Text,
            phone = PhoneTextBox.Text,
            password = PasswordBox.Password,
            repeatPassword = RepeatPasswordBox.Password;
        
        if (name.Length == 0
            || phone.Length == 0
            || password.Length == 0
            || repeatPassword.Length == 0)
        {
            h.showError("Введите имя, телефон, пароль и повторите его",
                "Ошибка регистрации");
        }
        else if (password != repeatPassword)
        {
            h.showError("Пароли не совпадают!",
                "Ошибка регистрации");
        }
        else if (phone.Length != 11 || phone.Any(char.IsLetter))
        {
            h.showError("Неверный номер телефона",
                "Ошибка регистрации");
        }
        else
        {
            try
            {
                var user = new user
                {
                    name = name, phone = phone, password = password, role = "u"
                };
                App.AppDbContext.users.Add(user);
                App.AppDbContext.SaveChanges();
                
                h.showEmpty("Вы зарегистрировались!", "Успех");
                App.User = App.AppDbContext.users.First(u => u.phone == phone);
                navigator.show<MainWindow>();
                Close();
            }
            catch (Exception ex)
            {
                h.showError(ex.Message);
                h.consoleLog(ex);
                h.debug(ex);
            }
        }
    }

    private void enterButton_OnClick(object sender, System.Windows.RoutedEventArgs e)
    {
        navigator.show<InWindow>();
        Close();
    }
}