using System.Windows;
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

namespace polina.classes;

public static class navigator
{
    private static Dictionary<Type, Window> windows { get; set; } = [];

    public static void show<T>() where T : Window, new()
    {
        var window = new T();
        windows[typeof(T)] = window;
        window.Closed += (_, _) => windows.Remove(typeof(T));
        window.Show();
    }

    public static void close<T>() where T : Window
    {
        if (!windows.TryGetValue(typeof(T), out var window)) return;
        window.Close();
        windows.Remove(typeof(T));
    }
}