using polina.classes;

namespace polina;

public partial class SellPage
{
    private string fullPath { get; set; } = null!;
    
    public SellPage()
    {
        InitializeComponent();
    }

    private void SellBtn_OnClick(object sender, System.Windows.RoutedEventArgs e)
    {
        try
        {
            var clothName = ClothName.Text;
            var clothDesc = ClothDesc.Text;
            var clothImage = ClothImage.Text;
            
            if (!double.TryParse(ClothPrice.Text, out var clothPrice))
            {
                throw new Exception($"Не удалось сконвертировать {ClothPrice.Text} в число");
            }
            
            if (clothName.Length == 0
                || clothDesc.Length == 0
                || clothImage.Length == 0)
            {
                throw new Exception("Заполни все поля");
            }
            
            System.IO.File.Copy(fullPath, "img/" + ClothImage.Text, true);

            try
            {
                if (h.showConfirm("Добавить?") == System.Windows.MessageBoxResult.Yes)
                {
                    App.AppDbContext.things.Add(new thing
                    {
                        name = clothName,
                        description = clothDesc,
                        image = "img/" + clothImage,
                        price = clothPrice,
                        seller = App.User.id
                    });
                    App.AppDbContext.SaveChanges();
                    h.showEmpty("Вещь успешно добавлена!", "Успех");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось добавить товар", ex);
            }
        }
        catch (Exception ex)
        {
            h.showError(ex.Message, "Ошибка добавления товара");
            h.debug(ex);
            h.consoleLog(ex);
        }
    }

    private void OpenImageBtn_OnClick(object sender, System.Windows.RoutedEventArgs e)
    {
        Microsoft.Win32.OpenFileDialog dialog = new()
        {
            Filter = "Images (*.jpg;*.png)|*.jpg;*.png"
        };
        
        if (dialog.ShowDialog() == true)
        {
            fullPath = dialog.FileName;
            ClothImage.Text = System.IO.Path.GetFileName(fullPath);
            System.Windows.Media.Imaging.BitmapImage image = new();
            image.BeginInit();
            image.UriSource = new Uri(fullPath);
            image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            image.EndInit();
            ImagePreview.Source = image;
        }
        else
        {
            ClothImage.Text = "";
            System.Windows.Media.Imaging.BitmapImage image = new();
            image.BeginInit();
            image.UriSource = new Uri("img/dflt.jpg", UriKind.Relative);
            image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            image.EndInit();
            ImagePreview.Source = image;
            if (System.IO.File.Exists(Environment.CurrentDirectory + "img/" + ClothImage.Text))
            {
                System.IO.File.Delete(Environment.CurrentDirectory + "img/" + ClothImage.Text);
            }
        }
    }
}