using polina.classes;
using System.Windows;

namespace polina;

public partial class BackupPage
{
    private class MessageInfo(String author, String message)
    {
        internal String Author = author;
        internal String Message = message;
        public override string ToString()
        {
            return $"{Author}: {Message}";
        }
    }

    public BackupPage()
    {
        InitializeComponent();
        Messages.ItemsSource = App
            .AppDbContext
            .messages
            .ToList()
            .Select(x => new MessageInfo(App.AppDbContext.users.First(u => u.id == x.sender).name, x.text));

        if (App.User.role == "m")
        {
            MessagesView.Visibility = Visibility.Visible;
            SendForm.Visibility = Visibility.Collapsed;
        }
        else
        {
            MessagesView.Visibility = Visibility.Collapsed;
            SendForm.Visibility = Visibility.Visible;
        }
    }

    private void SendBtn_OnClick(object sender, RoutedEventArgs e)
    {
        String text = MessageTextBox.Text;
        String mail = MailTextBox.Text;

        App.AppDbContext.messages.Add(new message
        {
            text = text, mail = mail, sender = App.User.id
        });
        h.showEmpty("Сообщение отправлено успешно!");
    }
}