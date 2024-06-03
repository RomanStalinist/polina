using polina.classes;

namespace polina;

public partial class App
{
    public static user User { get; set; } = null!;
    public static AppDbContext AppDbContext { get; } = new();

    protected override void OnStartup(System.Windows.StartupEventArgs e)
    {
        navigator.show<InWindow>();
    }

    protected override void OnExit(System.Windows.ExitEventArgs e)
    {
        AppDbContext.SaveChanges();
        AppDbContext.Dispose();
    }
}