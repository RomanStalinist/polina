namespace polina.classes;
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

public static class s
{
    public static string ToTitle(this string s) => s[0].ToString().ToUpper() + s[1..];
}