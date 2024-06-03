using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

namespace polina.classes;

public class user
{
    [Key] public int id { get; set; }
    [MaxLength(20)] public string name { get; set; } = null!;
    [StringLength(11)] public string phone { get; set; } = null!;
    [MaxLength(60)] public string password { get; set; } = null!;
    [StringLength(1)] public string role { get; set; } = null!;
    
    public override string ToString()
    {
        return  $"""
                Имя: {name}.
                Телефон: {phone}.
                Роль: {role}.
                """;
    }
}
