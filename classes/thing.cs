using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

namespace polina.classes;

public class thing
{
    [Key] public int id { get; set; }
    [MaxLength(50)] public string name { get; set; } = null!;
    [MaxLength(50)] public string description { get; set; } = null!;
    [MaxLength(255)] public string image { get; set; } = null!;
    [ForeignKey(name: "seller")] public user user { get; set; } = null!;
    public double price { get; set; }
    public int seller { get; set; }
    
    public string toString()
    {
        return $"""
               {name}.
               Цена: {price}.
               Описание: {description}.
               Продавец:
                   {user}
               """;
    }

    public override string ToString()
    {
        return $"{name.ToTitle()} - {price} руб.";
    }
}
