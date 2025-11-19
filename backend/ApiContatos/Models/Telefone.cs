using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiContatos.Models;

public class Telefone
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Numero { get; set; } = null!;

    [MaxLength(30)]
    public string? Tipo { get; set; }

    public int PessoaId { get; set; }

    [JsonIgnore] // não precisa vir no JSON
    public Pessoa? Pessoa { get; set; } // <- agora é opcional
}
