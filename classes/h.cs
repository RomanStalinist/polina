namespace polina.classes;

#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
public static class h
{
    public static void showEmpty(string message, string caption = "Сообщение")
    {
        System.Windows.MessageBox.Show(message, caption);
    }

    public static void showError(string message, string caption = "Ошибка")
    {
        System.Windows.MessageBox.Show(message, caption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
    }
    
    public static System.Windows.MessageBoxResult showConfirm(string message, string caption = "Подтверждение")
    {
        return System.Windows.MessageBox.Show(message, caption,
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);
    }

    public static void debug(object value)
    {
        System.Diagnostics.Debug.WriteLine(value.ToString());
    }

    public static void consoleLog(object value)
    {
        Console.WriteLine(value.ToString());
    }
}
