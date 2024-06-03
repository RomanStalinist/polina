using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

namespace polina.classes;

public class message
{
    [Key] public int id { get; set; }
    [MaxLength(100)] public string text { get; set; } = null!;
    [MaxLength(100)] public string mail { get; set; } = "sample@mail.ru";
    public int sender { get; set; }
    [ForeignKey(name: "sender")] public user user { get; set; } = null!;
}
