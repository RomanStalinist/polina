using polina.classes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;

namespace polina;

public partial class BuyPage
{
    private List<thing> clothes { get; set; }
    
    public BuyPage()
    {
        InitializeComponent();
        clothes = [.. App.AppDbContext.things];
        setListView();
    }

    private void buyPage_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) search(SearchBox.Text);
    }

    private void buyBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (ClothesView.SelectedItem is thing t)
        {
            buy(t);
        }
        else
        {
            h.showError("Вещь не выбрана!");
        }
    }

    private void searchBtn_OnClick(object sender, RoutedEventArgs e)
    {
        search(SearchBox.Text);
    }

    private void ClothesView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var cloth = (thing?)ClothesView.SelectedItem;
            if (cloth == null) throw new InvalidOperationException();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, cloth.image));
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            SelectedImage.Source = image;
            SelectedName.Text = cloth.name;
            SelectedDescription.Text = cloth.description;
        }
        catch (InvalidOperationException)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("img/dflt.jpg", UriKind.Relative);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            SelectedImage.Source = image;
            SelectedName.Text = "Название";
            SelectedDescription.Text = "Описание";
        }
        catch (Exception ex)
        {
            h.showError(ex.Message + ex.StackTrace);
        }
    }

    private void setCount(int count)
    {
        BuyClothBlock.Text = $"Купить (Найдено: {count})";
    }

    private void search(string query)
    {
        try
        {
            clothes = [.. App.AppDbContext.things.Where(
                c => EF.Functions.Like(string.Concat(c.name, c.description), $"%{query}%")
            )];
            setListView();
        }
        catch (Exception ex)
        {
            h.showError($"Товары по запросу '{query}' не найдены",
                "Ошибка поиска");
            h.debug(ex);
            h.consoleLog(ex);
        }
    }

    private static void buy(thing thing)
    {
        try
        {
            if (h.showConfirm("Уверен?") == MessageBoxResult.Yes)
            {
                Microsoft.Win32.SaveFileDialog sfd = new()
                {
                    FileName = thing.name,
                    Filter = "Images (*.jpg;*.png)|*.jpg;*.png"
                };
                
                if (sfd.ShowDialog() == true)
                {
                    System.IO.File.Copy(thing.image, sfd.FileName, true);
                    h.showEmpty($"Вы купили: {thing.toString()}");
                }
            }
        }
        catch (Exception ex)
        {
            h.showError($"Не удалось купить товар {thing.toString()}",
                "Ошибка покупки");
            h.debug(ex);
            h.consoleLog(ex);
        }
    }

    private void setListView()
    {
        ClothesView.ItemsSource = clothes;
        ClothesView.SelectedIndex = 0;
        setCount(clothes.Count);
    }
}