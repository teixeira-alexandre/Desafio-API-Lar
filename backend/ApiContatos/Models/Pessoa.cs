using System.ComponentModel.DataAnnotations;

namespace ApiContatos.Models;

public class Pessoa
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = null!;

    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(11)]
    public string? Cpf { get; set; }

    public DateTime? DataNascimento { get; set; }

    public bool EstaAtivo { get; set; } = true;

    // Relacionamento 1:N com Telefone
    public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
}
